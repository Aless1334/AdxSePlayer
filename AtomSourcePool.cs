using System.Collections.Generic;
using UnityEngine;

namespace AdxSePlayer
{
    public class AtomSourcePool : IObjectPool
    {
        public GameObject sourcePrefab { private get; set; }

        private List<CriAtomSource> _atomSourceList;

        public AtomSourcePool()
        {
            _atomSourceList = new List<CriAtomSource>();
        }

        public CriAtomSource Rent()
        {
            if (_atomSourceList.Count == 0)
            {
                return GenerateNewSource();
            }

            foreach (var atomSource in _atomSourceList)
            {
                if (atomSource.gameObject.activeSelf) continue;
                atomSource.gameObject.SetActive(true);
                return atomSource;
            }

            return GenerateNewSource();
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
