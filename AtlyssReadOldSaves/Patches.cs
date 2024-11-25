using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using AtlyssReadOldSaves.DataClasses;
using HarmonyLib;

namespace AtlyssReadOldSaves
{
    public static class Patches
    {
        [HarmonyPatch(typeof(ProfileDataManager), "Load_ProfileData")]
        [HarmonyPrefix]
        public static bool LoadProfileData(ProfileDataManager __instance, int __0, ref string ____dataPath, ref CharacterFile[] ____characterFiles)
        {
            if (!File.Exists(____dataPath + $"/characterProfile_{__0}"))
                return true;
            else
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Binder = new TypeConverter();
                Stream serializationStream = new FileStream(____dataPath + $"/characterProfile_{__0}", FileMode.Open, FileAccess.Read);
                var oldFormat = (OldCharacterFile) formatter.Deserialize(serializationStream);
                ____characterFiles[__0] = oldFormat.ToNewFile();
                serializationStream.Close();
                __instance.Delete_ProfileData();
                
                if (!File.Exists(____dataPath + $"/atl_characterProfile_{__0}"))
                    File.CreateText(____dataPath + $"/atl_characterProfile_{__0}");
            }

            return false;
        }
        
        [HarmonyPatch(typeof(ProfileDataManager), "Load_ItemStorageData")]
        [HarmonyPrefix]
        public static bool LoadItemStorageData(ProfileDataManager __instance, ref string ____dataPath, ref ItemStorage_Profile ____itemStorageProfile)
        {
            if (!File.Exists(____dataPath + "/itemBank"))
                return true;
            IFormatter formatter = new BinaryFormatter();
            formatter.Binder = new TypeConverter();
            Stream serializationStream = new FileStream(____dataPath + "/itemBank", FileMode.Open, FileAccess.Read);
            var oldFormat = (OldItemStorage_Profile)formatter.Deserialize(serializationStream);
            ____itemStorageProfile = oldFormat.ToNewStorageProfile();
            serializationStream.Close();

            if (!File.Exists(____dataPath + "/atl_itemBank"))
                File.CreateText(____dataPath + "/atl_itemBank");
            
            var current = ItemStorageManager._current;
            if (!current)
                return false;
            current._storageSizes[0] = 0;
            current._storageSizes[1] = 0;
            current._storageSizes[2] = 0;
            current._storageSizes_01[0] = 0;
            current._storageSizes_01[1] = 0;
            current._storageSizes_01[2] = 0;
            current._storageSizes_02[0] = 0;
            current._storageSizes_02[1] = 0;
            current._storageSizes_02[2] = 0;
            current._itemDatas.Clear();
            current._itemDatas_01.Clear();
            current._itemDatas_02.Clear();
            current._itemDatas.AddRange(____itemStorageProfile._heldItemStorage);

            return false;
        }
        
        [HarmonyPatch(typeof(ProfileDataManager), "Load_ProfileData")]
        [HarmonyPrefix]
        public static bool SaveProfileData()
        {
            Console.WriteLine("Saving Profile Data...");
            return true;
        }
    }
}