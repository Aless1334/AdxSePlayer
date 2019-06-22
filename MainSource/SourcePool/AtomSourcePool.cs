using System.Collections.Generic;
using UnityEngine;

namespace AdxSePlayer.SourcePool
{
    public class AtomSourcePool : IObjectPool
    {
        public GameObject sourcePrefab { private get; set; }

        private List<CriAtomSource> _atomSourceList;

        public AtomSourcePool(GameObject sourcePrefab)
        {
            _atomSourceList = new List<CriAtomSource>();
            this.sourcePrefab = sourcePrefab;
        }

        public CriAtomSource Rent()
        {
            if (_atomSourceList.Count == 0)
            {
                return SetDefaultParameter(GenerateNewSource());
            }

            foreach (var atomSource in _atomSourceList)
            {
                if (atomSource.gameObject.activeSelf) continue;
                atomSource.gameObject.SetActive(true);
                return SetDefaultParameter(atomSource);
            }

            return SetDefaultParameter(GenerateNewSource());
        }

        private CriAtomSource SetDefaultParameter(CriAtomSource source)
        {
            source.player.ResetParameters();
            return source;
        }

        private CriAtomSource GenerateNewSource()
        {
            var newSource = GameObject.Instantiate(sourcePrefab, Vector3.zero, Quaternion.identity)
                .GetComponent<CriAtomSource>();
            _atomSourceList.Add(newSource);
            return newSource;
        }

        public void Return(CriAtomSource target)
        {
            target.gameObject.SetActive(false);
        }
    }
}
