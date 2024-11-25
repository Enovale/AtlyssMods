using System;

namespace AtlyssReadOldSaves.DataClasses
{
    [Serializable]
    public class OldPlayerAppearance_Profile
    {
        public string _setRaceTag;
        public int _setEye;
        public int _setMouth;
        public int _setHairStyle;
        public int _setEar;
        public int _setTail;
        public int _setMisc;
        public bool _displayBoobs;
        public float _voicePitch;
        public int _helmDyeIndex;
        public int _chestDyeIndex;
        public int _legsDyeIndex;
        public int _capeDyeIndex;
        public float _headWidth;
        public float _muzzleWeight;
        public float _heightWeight = 1f;
        public float _widthWeight = 1f;
        public float _torsoWeight;
        public float _armWeight;
        public float _boobWeight;
        public float _bellyWeight;
        public float _bottomWeight;
        public int _setTexture;
        public bool _hideHelm = false;
        public bool _hideCape = false;
        public bool _hideChest = false;
        public bool _hideLeggings = false;
        public ColorAdjustShader_Profile _hairColorProfile;
        public bool _hairIsBodyColor = false;
        public ColorAdjustShader_Profile _miscColorProfile;
        public ColorAdjustShader_Profile _setSkinColorProfile;

        public PlayerAppearance_Profile ToNewProfile()
        {
            return new()
            {
                _setRaceTag = _setRaceTag,
                _setEye = _setEye,
                _setMouth = _setMouth,
                _setHairStyle = _setHairStyle,
                _setEar = _setEar,
                _setTail = _setTail,
                _setMisc = _setMisc,
                _displayBoobs = _displayBoobs,
                _isLeftHanded = false,
                _voicePitch = _voicePitch,
                _helmDyeIndex = _helmDyeIndex,
                _chestDyeIndex = _chestDyeIndex,
                _legsDyeIndex = _legsDyeIndex,
                _capeDyeIndex = _capeDyeIndex,
                _headWidth = _headWidth,
                _muzzleWeight = _muzzleWeight,
                _heightWeight = _heightWeight,
                _widthWeight = _widthWeight,
                _torsoWeight = _torsoWeight,
                _armWeight = _armWeight,
                _boobWeight = _boobWeight,
                _bellyWeight = _bellyWeight,
                _bottomWeight = _bottomWeight,
                _setTexture = _setTexture,
                _hideHelm = _hideHelm,
                _hideCape = _hideCape,
                _hideChest = _hideChest,
                _hideLeggings = _hideLeggings,
                _hairColorProfile = _hairColorProfile,
                _hairIsBodyColor = _hairIsBodyColor,
                _miscColorProfile = _miscColorProfile,
                _setSkinColorProfile = _setSkinColorProfile,
            };
        }
    }
}