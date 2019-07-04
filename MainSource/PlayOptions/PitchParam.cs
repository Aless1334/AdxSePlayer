namespace Aless.MainSource.PlayOptions
{
    public class PitchParam : IPlayOption
    {
        private float _pitch;

        public PitchParam(float pitch)
        {
            _pitch = pitch;
        }

        public CriAtomSource ApplySetting(CriAtomSource target)
        {
            target.pitch = _pitch;
            return target;
        }
    }
}