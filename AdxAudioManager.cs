using System.Collections.Generic;
using UnityEngine;

namespace Nagasono.AudioScripts.ADX
{
    public class AdxAudioManager : MonoBehaviour
    {
        private static AdxAudioManager _instance;

        [SerializeField] private bool _useObjectPoolInUniRx = false;
        [SerializeField] private CriAtomSource _sourcePrefab = null;
        [SerializeField] private string _cueSheetName = null;

        private CriAtomExAcb _cueAcb;

        private IObjectPool _sourcePool;

        private List<CriAtomSource> _activeSources;
        private List<CriAtomSource> _disableSourceBuffer;

        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;

            Initialize();
        }

        private void Initialize()
        {
            _cueAcb = CriAtom.GetAcb(_cueSheetName);

            if (_useObjectPoolInUniRx)
                _sourcePool = new AtomSourcePoolOnUniRx {sourcePrefab = _sourcePrefab.gameObject};
            else
                _sourcePool = new AtomSourcePool {sourcePrefab = _sourcePrefab.gameObject};

            _activeSources = new List<CriAtomSource>();
            _disableSourceBuffer = new List<CriAtomSource>();
        }

        private void Update()
        {
            _disableSourceBuffer.Clear();

            foreach (var activeSource in _activeSources)
            {
                if (activeSource.status != CriAtomSource.Status.PlayEnd) continue;

                _sourcePool.Return(activeSource);
                _disableSourceBuffer.Add(activeSource);
            }

            foreach (var source in _disableSourceBuffer)
            {
                _activeSources.Remove(source);
            }
        }

        // サウンド再生
        public static void PlayAudio(string key)
        {
            _instance.playAudio(key, 1.0f);
        }

        // 音量を指定してサウンド再生
        public static void PlayAudio(string key, float audioLevel, bool isConstant = false)
        {
            _instance.playAudio(key, audioLevel, isConstant);
        }

        private void playAudio(string key, float audioLevel, bool isConstant = false)
        {
            if (!_cueAcb.Exists(key))
            {
                UnityEngine.Debug.Log("Cue Not Found! :" + key);
                return;
            }

            var source = _sourcePool.Rent();
            source.cueSheet = _cueSheetName;
            source.cueName = key;
            source.volume = audioLevel;

            source.Play();

            _activeSources.Add(source);
        }
    }
}
