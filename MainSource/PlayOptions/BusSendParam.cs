using System;
using UnityEngine;

namespace AdxSePlayer.PlayOptions
{
    public class BusSendParam : IPlayOption
    {
        public enum SendLevelMode
        {
            Override,
            Offset,
        }

        private string _busSendTargetName;
        private float _busSendLevel;
        private SendLevelMode _levelMode;

        public BusSendParam(string busSendTargetName, float value, SendLevelMode levelMode = SendLevelMode.Override)
        {
            _busSendTargetName = busSendTargetName;
            _busSendLevel = Mathf.Clamp01(value);
            _levelMode = SendLevelMode.Override;
        }

        public CriAtomSource ApplySetting(CriAtomSource target)
        {
            switch (_levelMode)
            {
                case SendLevelMode.Override:
                    target.SetBusSendLevel(_busSendTargetName, _busSendLevel);
                    break;
                case SendLevelMode.Offset:
                    target.SetBusSendLevelOffset(_busSendTargetName, _busSendLevel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return target;
        }
    }
}