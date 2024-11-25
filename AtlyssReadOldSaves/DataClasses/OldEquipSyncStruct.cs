using System;

namespace AtlyssReadOldSaves.DataClasses
{
    [Serializable]
    public struct OldEquipSyncStruct
    {
        public string _helmTag;
        public int _helmModifier;
        public string _chestTag;
        public int _chestModifier;
        public string _leggingTag;
        public int _leggingModifier;
        public string _capeTag;
        public int _capeModifier;
        public string _ringTag;
        public int _ringModifier;
        public string _weaponTag;
        public int _weaponModifier;
        public string _altWeaponTag;
        public int _altWeaponModifier;
        public string _shieldTag;
        public int _shieldModifier;

        public EquipSyncStruct ToNewSyncStruct()
        {
            var equipSync = new EquipSyncStruct
            {
                _helmTag = _helmTag,
                _helmModifier = _helmModifier.ToString(),
                _chestTag = _chestTag,
                _chestModifier = _chestModifier.ToString(),
                _leggingTag = _leggingTag,
                _leggingModifier = _leggingModifier.ToString(),
                _capeTag = _capeTag,
                _capeModifier = _capeModifier.ToString(),
                _ringTag = _ringTag,
                _ringModifier = _ringModifier.ToString(),
                _weaponTag = _weaponTag,
                _weaponModifier = _weaponModifier.ToString(),
                _altWeaponTag = _altWeaponTag,
                _altWeaponModifier = _altWeaponModifier.ToString(),
                _shieldTag = _shieldTag,
                _shieldModifier = _shieldModifier.ToString()
            };

            return equipSync;
        }
    }
}