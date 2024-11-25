using System;
using System.Linq;

namespace AtlyssReadOldSaves.DataClasses
{
    [Serializable]
    public class OldItemStorage_Profile
    {
        public OldItemData[] _heldItemStorage;

        public ItemStorage_Profile ToNewStorageProfile() =>
            new()
            {
                _heldItemStorage = _heldItemStorage.Select(o => o.ToNewItemData()).ToArray()
            };
    }
}