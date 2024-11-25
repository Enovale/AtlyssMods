using System;
using System.Linq;
using UnityEngine;

namespace AtlyssReadOldSaves.DataClasses
{
    [Serializable]
    public class OldCharacterFile
    {
        public string _nickName;
        public string _version;
        public int _currency;
        public OldPlayerAppearance_Profile _appearanceProfile;
        public PlayerStats_Profile _statsProfile;
        public PlayerCasting_SkillsProfile _skillsProfile;
        public QuestProgressProfile _questProgressProfile;
        public OldItemData[] _inventoryProfile;
        public OldEquipSyncStruct _vanityStruct;
        public string[] _quickItemSlotProfile = new string[5];
        public string[] _dialogIntroProfile = [];
        public string[] _waypointAttunementProfile = [];
        public bool _isAltWeaponEquipped;
        public bool _isEmptySlot;

        public CharacterFile ToNewFile()
        {
            var file = new CharacterFile
            {
                _nickName = _nickName,
                _version = Application.version,
                //_version = _version,
                _currency = _currency,
                _appearanceProfile = _appearanceProfile.ToNewProfile(),
                _statsProfile = _statsProfile,
                _skillsProfile = _skillsProfile,
                _questProgressProfile = _questProgressProfile,
                _inventoryProfile = _inventoryProfile.Select(o => o.ToNewItemData()).ToArray(),
                _vanityStruct = _vanityStruct.ToNewSyncStruct(),
                _quickItemSlotProfile = _quickItemSlotProfile,
                _dialogIntroProfile = _dialogIntroProfile,
                _waypointAttunementProfile = _waypointAttunementProfile,
                _isAltWeaponEquipped = _isAltWeaponEquipped,
                _isEmptySlot = _isEmptySlot,
                _isNewCharacter = false
            };
            return file;
        }
    }
}