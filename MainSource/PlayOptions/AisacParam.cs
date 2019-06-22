using UnityEngine;

namespace AdxSePlayer.MainSource.PlayOptions
{
    public class AisacParam : IPlayOption
    {
        private uint _aisacTargetId;
        private string _aisacTargetName;
        private float _aisacControlvalue;
        private bool _isSearchById;

        public AisacParam(uint aisacTargetId, float aisacControlvalue)
        {
            _aisacTargetId = aisacTargetId;
            _aisacControlvalue = Mathf.Clamp01(aisacControlvalue);
            _isSearchById = true;
        }

        public AisacParam(string targetName, float aisacControlvalue)
        {
            _aisacTargetName = targetName;
            _aisacControlvalue = Mathf.Clamp01(aisacControlvalue);
            _isSearchById = false;
        }

        public CriAtomSource ApplySetting(CriAtomSource target)
        {
            if (_isSearchById)
                target.SetAisacControl(_aisacTargetId, _aisacControlvalue);
            else
                target.SetAisacControl(_aisacTargetName, _aisacControlvalue);
            return target;
        }
    }
}