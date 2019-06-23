using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace AdxSePlayer.MainSource
{
    public static class UsingAcbData
    {
        private static CriAtomExAcb[] _acbArray;

        private static string[] _proceedStrings;

        public static CriAtomExAcb[] AcbArray
        {
            get
            {
                CallUpdateAcbInfo();
                return _acbArray;
            }
        }

        private static void UpdateAcbInfo(CriAtom atomComponent)
        {
            if (atomComponent == null)
            {
                Debug.LogError("CriAtom コンポーネントがシーン内に存在しません。");
                return;
            }

            CriAtomEx.UnregisterAcf();
            CriAtomPlugin.InitializeLibrary();

            _acbArray = new CriAtomExAcb[atomComponent.cueSheets.Length];
            for (var i = 0; i < _acbArray.Length; i++)
            {
                _acbArray[i] = CriAtomExAcb.LoadAcbFile(null,
                    Application.streamingAssetsPath + "/" + atomComponent.cueSheets[i].acbFile,
                    Application.streamingAssetsPath + "/" + atomComponent.cueSheets[i].awbFile);
            }
        }

        private static void CallUpdateAcbInfo()
        {
            var atomComponent = Object.FindObjectOfType<CriAtom>();

            var sheetNameList = GetCueSheetNameArray(atomComponent);

            if (_proceedStrings == null || _acbArray == null)
            {
                _proceedStrings = sheetNameList;
                UpdateAcbInfo(atomComponent);
                return;
            }

            if (_proceedStrings.Length != sheetNameList.Length)
            {
                _proceedStrings = sheetNameList;
                UpdateAcbInfo(atomComponent);
                return;
            }

            for (var i = 0; i < _proceedStrings.Length; i++)
            {
                if (_proceedStrings[i].Equals(sheetNameList[i])) continue;

                _proceedStrings = sheetNameList;
                UpdateAcbInfo(atomComponent);
                return;
            }
        }

        private static string[] GetCueSheetNameArray(CriAtom atomComponent)
        {
            var sheetList = atomComponent.cueSheets;
            var sheetNameList = new string[sheetList.Length];
            for (var i = 0; i < sheetList.Length; i++)
                sheetNameList[i] = sheetList[i].name;

            return sheetNameList;
        }
    }
}