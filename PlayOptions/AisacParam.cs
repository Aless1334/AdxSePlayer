namespace AdxSePlayer.PlayOptions
{
    public class AisacParam : IPlayOption
    {
        private uint _aisacTargetId;
        private string _aisacTargetName;
        private float _aisacControlValue;
        private bool _isSearchById;

        public AisacParam(uint aisacTargetId, float aisacControlValue)
        {
            _aisacTargetId = aisacTargetId;
            _aisacControlValue = aisacControlValue;
            _isSearchById = true;
        }

        public AisacParam(string targetName, float value)
        {
            _aisacTargetName = targetName;
            _aisacControlValue = value;
            _isSearchById = false;
        }

        public CriAtomSource ApplySetting(CriAtomSource target)
        {
            if (_isSearchById)
                target.SetAisacControl(_aisacTargetId, _aisacControlValue);
            else
                target.SetAisacControl(_aisacTargetName, _aisacControlValue);
            return target;
        }
    }
}