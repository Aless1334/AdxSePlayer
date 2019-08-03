using System.Collections.Generic;
using System.Linq;
using Aless.MainSource.PlayOptions;
using Aless.MainSource.SourcePool;
using UnityEngine;

namespace Aless.MainSource
{
    public class AdxSePlayer : MonoBehaviour
    {
        private static AdxSePlayer _instance;

        [SerializeField] private bool _useObjectPoolInUniRx = false;
        [SerializeField] private CriAtomSource _sourcePrefab = null;
        [SerializeField] private string[] _cueSheetNames = null;

        private CriAtomExAcb[] _cueAcbArray;

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
            _cueAcbArray = new CriAtomExAcb[_cueSheetNames.Length];
            for (var i = 0; i < _cueAcbArray.Length; i++)
            {
                _cueAcbArray[i] = CriAtom.GetAcb(_cueSheetNames[i]);
            }

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

        // サウンド再生(option設定可能)
        public static void PlayAudio(string key, params IPlayOption[] options)
        {
            _instance?.playAudio(key, options);
        }

        // サウンド再生(option設定可能)
        public static void PlayAudio(string sheetName, string key, params IPlayOption[] options)
        {
            _instance?.playAudio(key, sheetName, options);
        }

        private void playAudio(string key, params IPlayOption[] options)
        {
//            var targetAcb = _cueAcbArray.FirstOrDefault(cueAcb => cueAcb.Exists(key));

            CriAtomExAcb targetAcb = null;
            string targetSheetName = null;
            
            for (var i = 0; i < _cueAcbArray.Length; i++)
            {
                if (!_cueAcbArray[i].Exists(key)) continue;
                targetAcb = _cueAcbArray[i];
                targetSheetName = _cueSheetNames[i];
            }

            if (targetAcb == null || targetSheetName == null)
            {
#if DEBUG
                UnityEngine.Debug.LogWarning("Cue Not Found! :" + key);
#endif
                return;
            }

            var source = _sourcePool.Rent();
            source.cueSheet = targetSheetName;
            source.cueName = key;

            foreach (var option in options)
            {
                option.ApplySetting(source);
            }

            source.Play();

            _activeSources.Add(source);
        }
        
        private void playAudio(string key, string targetSheetName, params IPlayOption[] options)
        {
            CriAtomExAcb targetAcb = null;
            
            if (targetSheetName != null)
            {
                for (var i = 0; i < _cueAcbArray.Length; i++)
                {
                    if (!targetSheetName.Equals(_cueSheetNames[i])) continue;
                    
                    targetAcb = _cueAcbArray[i];
                    break;
                }
            }
            else
            {
                foreach (var cueAcb in _cueAcbArray)
                {
                    if (!cueAcb.Exists(key)) continue;
                    targetAcb = cueAcb;
                    break;
                }
            }

            if (targetAcb == null)
            {
#if DEBUG
                UnityEngine.Debug.LogWarning("Acb Not Found: " + targetSheetName);
#endif
                return;
            }

            if (!targetAcb.Exists(key))
            {
#if DEBUG
                UnityEngine.Debug.LogWarning("Cue Not Found! :" + key);
#endif
                return;
            }

            var source = _sourcePool.Rent();
            source.cueSheet = targetSheetName;
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
