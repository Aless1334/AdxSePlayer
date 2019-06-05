using System.Collections.Generic;
using AdxSePlayer.PlayOptions;
using UnityEngine;

namespace AdxSePlayer
{
    public class AdxSePlayer : MonoBehaviour
    {
        private static AdxSePlayer _instance;

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
                _sourcePool = new AtomSourcePoolOnUniRx(_sourcePrefab.gameObject);
            else
                _sourcePool = new AtomSourcePool(_sourcePrefab.gameObject);

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

        // サウンド再生(volume, pitch設定可能)
        public static void PlayAudio(string key, params IPlayOption[] options)
        {
            _instance?.playAudio(key, options);
        }

        private void playAudio(string key, params IPlayOption[] options)
        {
            if (!_cueAcb.Exists(key))
            {
#if DEBUG
                UnityEngine.Debug.Log("Cue Not Found! :" + key);
#endif
                return;
            }

            var source = _sourcePool.Rent();
            source.cueSheet = _cueSheetName;
            source.cueName = key;

            foreach (var option in options)
            {
                option.ApplySetting(source);
            }
            
            source.Play();

            _activeSources.Add(source);
        }
    }
}
