using UnityEditor;
using UnityEngine;

namespace AdxSePlayer.MainSource.Support.Editor
{
    [CustomPropertyDrawer(typeof(CueIndex))]
    public class CueSheetDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var atomComponent = Object.FindObjectOfType<CriAtom>();

            var sheetNames = new string[atomComponent.cueSheets.Length];
            for (var i = 0; i < sheetNames.Length; i++)
                sheetNames[i] = atomComponent.cueSheets[i].name;

            EditorGUI.LabelField(position, property.name);

            EditorGUI.BeginChangeCheck();

            var selectedIndex = property.FindPropertyRelative("selectedIndex");
            var changedIndex =
                EditorGUI.Popup(
                    new Rect(EditorGUIUtility.labelWidth, position.y, position.width - EditorGUIUtility.labelWidth,
                        EditorGUIUtility.singleLineHeight), selectedIndex.intValue, sheetNames);

            if (EditorGUI.EndChangeCheck())
            {
                selectedIndex.intValue = changedIndex;
                property.FindPropertyRelative("selectedName").stringValue = sheetNames[changedIndex];
            }
        }
    }
}