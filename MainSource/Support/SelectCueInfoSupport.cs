using UnityEngine;

namespace Aless.MainSource.Support
{
    [RequireComponent(typeof(CriAtomSource))]
    public class SelectCueInfoSupport : MonoBehaviour
    {
        private CriAtomSource _atomSource;

        private string _cueSheetName = null;

        public string cueSheetName
        {
            get { return _cueSheetName; }
            set
            {
                if (!_atomSource) _atomSource = GetComponent<CriAtomSource>();

                _cueSheetName = value;
                _atomSource.cueSheet = _cueSheetName;
            }
        }

        private string _cueName = null;
        
        public string cueName
        {
            get { return _cueName; }
            set
            {
                if (!_atomSource) _atomSource = GetComponent<CriAtomSource>();

                _cueName = value;
                _atomSource.cueName = _cueName;
            }
        }

        public int selectedSheetIndex = 0;
        public int selectedCueIndex = 0;
    }
}
