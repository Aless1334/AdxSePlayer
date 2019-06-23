using AdxSePlayer.MainSource.Support;
using UnityEngine;

namespace AdxSePlayer.MainSource.AttachComponents
{
    public class PlaySeInfo : MonoBehaviour
    {
        public string cueSheetName { get; set; } = null;
        public string cueName { get; set; } = null;

        public int selectedSheetIndex = 0;
        public int selectedCueIndex = 0;
    }
}
