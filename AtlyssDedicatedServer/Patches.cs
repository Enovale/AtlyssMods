using System.IO;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Mirror;
using Mirror.FizzySteam;
using UnityEngine;

namespace AtlyssDedicatedServer
{
    public static class Patches
    {
        private const string batchModeProfilePath = "/server_profileCollections/";
        
        [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Awake))]
        [HarmonyPrefix]
        public static void AtlyssNetworkManager_Awake()
        {
            if (Plugin.ConfigShouldUseSteamworks.Value)
                return;
            
            var telepathy = Object.FindObjectsOfType<AtlyssNetworkManager>(true)
                .First(o => o.gameObject.GetComponent<TelepathyTransport>() != null);
            telepathy.gameObject.SetActive(true);
            var fizzySteamworks = Object.FindObjectsOfType<AtlyssNetworkManager>(true)
                .First(o => o.gameObject.GetComponent<FizzySteamworks>() != null);
            fizzySteamworks.gameObject.SetActive(false);

            var networkManager = telepathy.gameObject.GetComponent<AtlyssNetworkManager>();
            networkManager._steamworksMode = false;
            networkManager._soloMode = false;
            networkManager._serverMode = true;
            telepathy.gameObject.GetComponent<MapInstancingManager>()._hubMap = fizzySteamworks.gameObject.GetComponent<MapInstancingManager>()._hubMap;
        }

        [HarmonyPatch(typeof(ProfileDataManager), nameof(ProfileDataManager.Start))]
        [HarmonyPostfix]
        public static void ProfileDataStart()
        {
            if (Application.isBatchMode)
            {
                MainMenuManager._current.Set_HostMode((int)NetworkInitCondition.Host_Multiplayer);
                ProfileDataManager._current.Set_FileIndex(0);
                MainMenuManager._current._characterSelectManager.Select_CharacterFile();
            }
        }

        [HarmonyPatch(typeof(ProfileDataManager), nameof(ProfileDataManager.Awake))]
        [HarmonyPostfix]
        public static void ProfileDataManager_Awake(ProfileDataManager __instance)
        {
            if (Application.isBatchMode)
            {
                ProfileDataManager._current._dataPath = Application.dataPath + batchModeProfilePath;
            }
        }

        [HarmonyPatch(typeof(SettingsManager), nameof(SettingsManager.Awake))]
        [HarmonyPostfix]
        public static void SettingsManager_Awake(SettingsManager __instance)
        {
            if (Application.isBatchMode)
            {
                SettingsManager._current._dataPath = Application.dataPath + batchModeProfilePath;
            }
        }

        [HarmonyPatch(typeof(LobbyListManager), nameof(LobbyListManager.Awake))]
        [HarmonyPostfix]
        public static void LobbyListManager_Awake(LobbyListManager __instance)
        {
            __instance._lobbyMaxConnectionSlider.maxValue = 999999;
        }

        [HarmonyPatch(typeof(ProfileDataManager), nameof(ProfileDataManager.Load_HostSettingsData))]
        [HarmonyPostfix]
        public static void ProfileDataManager_Load_HostSettingsData(ProfileDataManager __instance)
        {
            if (!File.Exists(__instance._dataPath + "/hostSettings.json"))
                return;
            
            __instance._hostSettingsProfile = JsonUtility.FromJson<ServerHostSettings_Profile>(File.ReadAllText(__instance._dataPath + "/hostSettings.json"));
            if (__instance._hostSettingsProfile._maxAllowedConnections < 2)
                __instance._hostSettingsProfile._maxAllowedConnections = 2;
            MainMenuManager._current.Load_HostSettings();
        }

        [HarmonyPatch(typeof(SettingsManager), nameof(SettingsManager.Load_SettingsData))]
        [HarmonyPostfix]
        public static void SettingsManager_Load_SettingsData(SettingsManager __instance)
        {
            if (Application.isBatchMode)
            {
                __instance._voiceVolumeSlider.value = 0;
                __instance._ambienceVolumeSlider.value = 0;
                __instance._gameVolumeSlider.value = 0;
                __instance._guiVolumeSlider.value = 0;
                __instance._masterVolumeSlider.value = 0;
                __instance._musicVolumeSlider.value = 0;
                Application.targetFrameRate = Plugin.ConfigTickRate.Value;
            }
        }

        [HarmonyPatch(typeof(AtlyssNetworkManager), nameof(AtlyssNetworkManager.Awake))]
        [HarmonyPostfix]
        public static void AtlyssNetworkManager_Awake(AtlyssNetworkManager __instance)
        {
            if (Application.isBatchMode)
            {
                __instance.sendRate = Plugin.ConfigTickRate.Value;
            }
        }

        [HarmonyPatch(typeof(ChatBehaviour), nameof(ChatBehaviour.UserCode_Rpc_RecieveChatMessage__String__Boolean__ChatChannel))]
        [HarmonyPostfix]
        public static void ChatBehaviour_UserCode_Rpc_RecieveChatMessage__String__Boolean__ChatChannel(ChatBehaviour __instance,
            string __0, ChatBehaviour.ChatChannel __2)
        {
            Plugin.Logger.LogWarning($"[{__2}] {__instance._player._nickname}: {__0}");
        }
    }
}