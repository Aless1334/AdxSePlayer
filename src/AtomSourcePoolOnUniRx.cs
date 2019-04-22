using UniRx.Toolkit;
using UnityEngine;

namespace Nagasono.AudioScripts.ADX
{
    public class AtomSourcePoolOnUniRx : ObjectPool<CriAtomSource>, IObjectPool
    {
        public GameObject sourcePrefab { private get; set; }

        protected override CriAtomSource CreateInstance()
        {
            return GameObject.Instantiate(sourcePrefab, Vector3.zero, Quaternion.identity)
                .GetComponent<CriAtomSource>();
        }
    }
}
