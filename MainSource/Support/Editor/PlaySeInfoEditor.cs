using AdxSePlayer.MainSource.AttachComponents;
using AdxSePlayer.MainSource.DataManage;
using UnityEditor;

namespace AdxSePlayer.MainSource.Support.Editor
{
    [CustomEditor(typeof(PlaySeInfo))]
    public class PlaySeInfoEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var playSeInfo = target as PlaySeInfo;
            if (!playSeInfo) return;

            if (!UsingAcbData.IsHavingAtomComponent) return;

            // 数値バッファリング
            var lastSheetIndex = playSeInfo.selectedSheetIndex;
            var lastCueIndex = playSeInfo.selectedCueIndex;

            playSeInfo.selectedSheetIndex =
                EditorGUILayout.Popup("Cue Sheet", playSeInfo.selectedSheetIndex, UsingAcbData.CueSheetNameArray);

            var cueNames = UsingAcbData.LoadedAcbDataList[playSeInfo.selectedSheetIndex].CueNames;
            if (cueNames == null)
                EditorGUILayout.LabelField("Acb Can't Load.");
            else
                playSeInfo.selectedCueIndex =
                    EditorGUILayout.Popup("Cue Name", playSeInfo.selectedCueIndex, cueNames);

            // 変更があった場合、AtomSourceの値を変更
            if (lastSheetIndex != playSeInfo.selectedSheetIndex)
            {
                ChangeCueSheetName(playSeInfo);
            }
            else if (cueNames != null && lastCueIndex != playSeInfo.selectedCueIndex)
            {
                playSeInfo.cueName = cueNames[playSeInfo.selectedCueIndex];
            }

            // 変更の反映
            EditorUtility.SetDirty(playSeInfo);
        }

        private void ChangeCueSheetName(PlaySeInfo info)
        {
            info.cueSheetName = UsingAcbData.CueSheetNameArray[info.selectedSheetIndex];
            info.selectedCueIndex = 0;
            var selectAcb = UsingAcbData.AcbArray[info.selectedSheetIndex];
            if (selectAcb != null)
                info.cueName = selectAcb
                    .GetCueInfoList()[info.selectedCueIndex].name;
        }
    }
}