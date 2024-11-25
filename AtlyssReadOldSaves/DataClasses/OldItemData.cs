using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AtlyssReadOldSaves.DataClasses
{
    [Serializable]
    public class OldItemData
    {
        public string _itemName;
        public int _quantity = 1;
        public int _maxQuantity = 1;
        public int _slotNumber = 0;
        public int _modifierIndex = -1;
        [Space]
        public bool _isEquipped = false;
        public bool _isAltWeapon = false;

        public ItemData ToNewItemData()
        {
            var modListField = typeof(GameManager).GetField("_cachedScriptableStatModifiers",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var modifierTag = string.Empty;
            bool shouldTryToParseModifiers = false;
            if (shouldTryToParseModifiers)
            {
                var scriptableStatModifierArray = modListField.GetValue(GameManager._current) as Dictionary<string, ScriptableStatModifier>;
                if (_modifierIndex > -1 && scriptableStatModifierArray.Count > _modifierIndex)
                    modifierTag = scriptableStatModifierArray.Keys.ToArray()[_modifierIndex];
            }
            return new()
            {
                _itemName = _itemName,
                _quantity = _quantity,
                _maxQuantity = _maxQuantity,
                _slotNumber = _slotNumber,
                _modifierTag = modifierTag,
                _isEquipped = _isEquipped,
                _isAltWeapon = _isAltWeapon
            };
        }
    }
}