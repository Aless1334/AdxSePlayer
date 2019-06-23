using System.Reflection;
using UnityEditor;

namespace AdxSePlayer.MainSource.Support.Editor
{
    [CustomEditor(typeof(SelectCueInfoSupport))]
    public class SelectCueInfoSupportEditor : UnityEditor.Editor
    {
        private CriAtom _atomObject;

        public override void OnInspectorGUI()
        {
            var selectCue = target as SelectCueInfoSupport;
            if (!selectCue) return;

            if (!_atomObject)
            {
                _atomObject = PrepareAtom();
                if (!_atomObject)
                {
                    EditorGUILayout.LabelField("Please make CriAtom in Hierarchy.");
                    return;
                }
            }

            // キューシート名のリストを取得
            var cueSheetNames = GetCueSheetNameArray();
            
            // 選択したシートのキュー名のリストを取得
            var targetAcb = UsingAcbData.AcbArray[selectCue.selectedSheetIndex];
            
            var cueNames = LoadCueNameArray(targetAcb);

            // 数値バッファリング
            var lastSheetIndex = selectCue.selectedSheetIndex;
            var lastCueIndex = selectCue.selectedCueIndex;

            selectCue.selectedSheetIndex =
                EditorGUILayout.Popup("Cue Sheet", selectCue.selectedSheetIndex, cueSheetNames);

            if (cueNames == null)
                EditorGUILayout.LabelField("Acb Can't Load.");
            else
                selectCue.selectedCueIndex =
                    EditorGUILayout.Popup("Cue Name", selectCue.selectedCueIndex, cueNames);

            // 変更があった場合、AtomSourceの値を変更
            if (lastSheetIndex != selectCue.selectedSheetIndex)
            {
                selectCue.cueSheetName = _atomObject.cueSheets[selectCue.selectedSheetIndex].name;
                selectCue.selectedCueIndex = 0;
                var selectAcb = UsingAcbData.AcbArray[selectCue.selectedSheetIndex];
                if (selectAcb != null)
                    selectCue.cueName = selectAcb
                        .GetCueInfoList()[selectCue.selectedCueIndex].name;
            }
            else if (cueNames != null && lastCueIndex != selectCue.selectedCueIndex)
            {
                selectCue.cueName = cueNames[selectCue.selectedCueIndex];
            }

            // 変更の反映
            EditorUtility.SetDirty(selectCue);
        }

        private static CriAtom PrepareAtom()
        {
            CriAtomEx.UnregisterAcf();
            CriAtomPlugin.InitializeLibrary();

            return FindObjectOfType<CriAtom>();
        }

        private string[] GetCueSheetNameArray()
        {
            var sheetList = _atomObject.cueSheets;
            var sheetNameList = new string[sheetList.Length];
            for (var i = 0; i < sheetList.Length; i++)
                sheetNameList[i] = sheetList[i].name;

            return sheetNameList;
        }

//        private CriAtomExAcb GetAcbData(string targetAcbName, string targetAwbName = "")
//        {
//            return (CriAtomExAcb) _atomObject.GetType().InvokeMember("LoadAcbFile",
//                BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance, null, _atomObject, new[]
//                {
//                    null, targetAcbName, targetAwbName
//                });
//        }

        private static string[] LoadCueNameArray(CriAtomExAcb acbData)
        {
            if (acbData == null) return null;
            var cueInfoList = acbData.GetCueInfoList();
            var cueNames = new string[cueInfoList.Length];

            for (var i = 0; i < cueInfoList.Length; i++)
                cueNames[i] = cueInfoList[i].name;

            return cueNames;
        }
    }
}
