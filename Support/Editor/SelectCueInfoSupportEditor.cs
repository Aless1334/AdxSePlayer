using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AdxSePlayer.Support.Editor
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
                CriAtomEx.UnregisterAcf();
                CriAtomPlugin.InitializeLibrary();
                
                _atomObject = FindObjectOfType<CriAtom>();
                if (!_atomObject)
                {
                    EditorGUILayout.LabelField("Please make CriAtom in Hierarchy.");
                    return;
                }
            }

            var cueSheetNames = new string[_atomObject.cueSheets.Length];
            for (var i = 0; i < cueSheetNames.Length; i++)
                cueSheetNames[i] = _atomObject.cueSheets[i].name;

            var atom = _atomObject.GetType();
            var func = (CriAtomExAcb) atom.InvokeMember("LoadAcbFile",
                BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance, null, _atomObject, new[]
                {
                    null,
                    _atomObject.cueSheets[selectCue.selectedSheetIndex].acbFile,
                    _atomObject.cueSheets[selectCue.selectedSheetIndex].awbFile
                });

            var cueNames = new string[func.GetCueInfoList().Length];
            for (var i = 0; i < cueNames.Length; i++)
                cueNames[i] = func.GetCueInfoList()[i].name;

            var lastSheetIndex = selectCue.selectedSheetIndex;
            var lastCueIndex = selectCue.selectedCueIndex;

            selectCue.selectedSheetIndex =
                EditorGUILayout.Popup("Cue Sheet", selectCue.selectedSheetIndex, cueSheetNames);

            selectCue.selectedCueIndex =
                EditorGUILayout.Popup("Cue Name", selectCue.selectedCueIndex, cueNames);

            if (lastSheetIndex != selectCue.selectedSheetIndex)
            {
                selectCue.cueSheetName = _atomObject.cueSheets[selectCue.selectedSheetIndex].name;
            }
            
            if (lastCueIndex != selectCue.selectedCueIndex)
            {
                selectCue.cueName = cueNames[selectCue.selectedCueIndex];
            }

            EditorUtility.SetDirty(selectCue);
        }
    }
}
