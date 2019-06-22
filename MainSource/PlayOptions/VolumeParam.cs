namespace AdxSePlayer.MainSource.PlayOptions
{
    public class VolumeParam : IPlayOption
    {
        private float _volume;

        public VolumeParam(float volume)
        {
            _volume = volume;
        }

        public CriAtomSource ApplySetting(CriAtomSource target)
        {
            target.volume = _volume;
            return target;
        }
    }
}