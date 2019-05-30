namespace AdxSePlayer.PlayOptions
{
    public class BusSendParam:IPlayOption
    {
        private string _busSendTargetName;
        private float _busSendLevel;

        public BusSendParam(string busSendTargetName, float value)
        {
            _busSendTargetName = busSendTargetName;
            _busSendLevel = value;
        }

        public CriAtomSource ApplySetting(CriAtomSource target)
        {
            target.SetBusSendLevel(_busSendTargetName, _busSendLevel);
            return target;
        }
    }
}