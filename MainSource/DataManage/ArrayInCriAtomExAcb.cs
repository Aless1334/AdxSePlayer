namespace Aless.MainSource.DataManage
{
    public class ArrayInCriAtomExAcb
    {
        private CriAtomExAcb _targetAcb;

        public string[] CueNames { get; private set; }

        public ArrayInCriAtomExAcb(CriAtomExAcb targetAcb)
        {
            _targetAcb = targetAcb;
            RefreshInfo();
        }

        private string[] LoadCueNames()
        {
            var cueInfos = _targetAcb.GetCueInfoList();
            var cueNames = new string[cueInfos.Length];

            for (var i = 0; i < cueInfos.Length; i++)
            {
                cueNames[i] = cueInfos[i].name;
            }

            return cueNames;
        }

        public void RefreshInfo()
        {
            CueNames = LoadCueNames();
        }
    }
}