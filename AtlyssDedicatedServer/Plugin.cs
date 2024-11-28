using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Mirror;
using UnityEngine;

namespace AtlyssDedicatedServer;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    
    public static ConfigEntry<bool> ConfigShouldUseSteamworks;
    public static ConfigEntry<int> ConfigTickRate;
    public static ConfigEntry<bool> ConfigServerOnlyMode;

    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;

        ConfigShouldUseSteamworks = Config.Bind("General", "ShouldUseSteamworks", false,
            "Whether or not to make a steam lobby for vanilla players or a port forwarded telepathy server.");
        ConfigTickRate = Config.Bind("General", "TickRate", 70, "The tick rate of the server.");
        ConfigServerOnlyMode = Config.Bind("General", "ServerOnlyMode", false,
            "Enables ServerOnly mode which doesn't require a host character.");
        Harmony.CreateAndPatchAll(typeof(Patches));
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            MainMenuManager._current.Init_MultiplayerServer();
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            var joinDolly = GameObject.Find("Canvas_gameStart").transform.GetChild(2);
            joinDolly.gameObject.SetActive(true);
            joinDolly.GetComponent<MenuElement>().EnableElement(true);
            //MainMenuManager._current.Connect_ToServer();
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            MainMenuManager._current.Init_MultiplayerHost();
        }
    }
}