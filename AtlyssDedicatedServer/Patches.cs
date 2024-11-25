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
        [HarmonyPatch(typeof(MainMenuManager), "Awake")]
        [HarmonyPrefix]
        public static void AtlyssNetworkManager_Awake()
        {
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

        [HarmonyPatch(typeof(ProfileDataManager), "Start")]
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

        [HarmonyPatch(typeof(ProfileDataManager), "Awake")]
        [HarmonyPostfix]
        public static void ProfileDataManager_Awake(ProfileDataManager __instance)
        {
            if (Application.isBatchMode)
            {
                var field = typeof(ProfileDataManager).GetField("_dataPath",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                field.SetValue(__instance, Application.persistentDataPath);
            }
        }

        [HarmonyPatch(typeof(SettingsManager), "Awake")]
        [HarmonyPostfix]
        public static void SettingsManager_Awake(SettingsManager __instance)
        {
            if (Application.isBatchMode)
            {
                var field = typeof(SettingsManager).GetField("_dataPath",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                field.SetValue(__instance, Application.persistentDataPath + "/profileCollections/");
            }
        }
    }
}