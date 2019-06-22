using System;
using UnityEngine;

namespace AdxSePlayer.MainSource
{
    [Serializable]
    public class CueIndex
    {
        public string selectedName;
        [SerializeField] private int selectedIndex;
    }
}