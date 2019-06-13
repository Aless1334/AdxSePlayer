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
                _atomObject = FindObjectOfType<CriAtom>();
                if (!_atomObject)
                {
                    EditorGUILayout.LabelField("Please make CriAtom in Hierarchy.");
                    return;
                }
            }

            var _cueSheetNames = new string[_atomObject.cueSheets.Length];
            for (var i = 0; i < _cueSheetNames.Length; i++)
                _cueSheetNames[i] = _atomObject.cueSheets[i].name;

            selectCue.selectedSheetIndex =
                EditorGUILayout.Popup("Cue Sheet", selectCue.selectedSheetIndex, _cueSheetNames);

            EditorUtility.SetDirty(selectCue);
        }
    }
}
