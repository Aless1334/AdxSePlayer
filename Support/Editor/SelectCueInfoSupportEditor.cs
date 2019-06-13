using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AdxSePlayer.Support.Editor
{
    [CustomEditor(typeof(SelectCueInfoSupport))]
    public class SelectCueInfoSupportEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var selectCue = target as SelectCueInfoSupport;
            if (!selectCue) return;

            selectCue.selectedSheetIndex = EditorGUILayout.Popup(selectCue.selectedSheetIndex, new string[] { });

            var names = typeof(CriAtom).GetProperty("instance",
                BindingFlags.Static | BindingFlags.NonPublic);



            Debug.Log(names.GetValue("cueSheets", BindingFlags.NonPublic));
        }
    }
}
