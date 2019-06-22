using UnityEngine;

namespace AdxSePlayer.MainSource
{
    public class PlaySeInfo : MonoBehaviour
    {
        [SerializeField] private CueIndex _cueSheetName = new CueIndex();
        [SerializeField] private string _cueName = null;
    }
}
