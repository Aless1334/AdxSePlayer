﻿using System.Reflection;
using AdxSePlayer.MainSource.AttachComponents;
using AdxSePlayer.MainSource.DataManage;
using UnityEditor;

namespace AdxSePlayer.MainSource.Support.Editor
{
    [CustomEditor(typeof(SelectCueInfoSupport))]
    public class SelectCueInfoSupportEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var selectCue = target as SelectCueInfoSupport;
            if (!selectCue) return;

            if (!UsingAcbData.IsHavingAtomComponent) return;

            // キューシート名のリストを取得
            var cueSheetNames = UsingAcbData.CueSheetNameArray;
            
            // 選択したシートのキュー名のリストを取得
            var cueNames = UsingAcbData.LoadedAcbDataList[selectCue.selectedSheetIndex].CueNames;

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
                selectCue.cueSheetName = cueSheetNames[selectCue.selectedSheetIndex];
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
    }
}
