using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace AtlyssDedicatedServer;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    
    public static ConfigEntry<bool> ConfigShouldUseSteamworks;

    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;

        ConfigShouldUseSteamworks = Config.Bind("General", "ShouldUseSteamworks", false,
            "Whether or not to make a steam lobby for vanilla players or a port forwarded telepathy server.");
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