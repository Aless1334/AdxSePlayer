using UniRx.Toolkit;
using UnityEngine;

namespace Aless.MainSource.SourcePool
{
    public class SourceUniRxPoolOrigin : ObjectPool<CriAtomSource>
    {
        public GameObject sourcePrefab { private get; set; }

        public SourceUniRxPoolOrigin(GameObject sourcePrefab) : base()
        {
            this.sourcePrefab = sourcePrefab;
        }

        protected override CriAtomSource CreateInstance()
        {
            return GameObject.Instantiate(sourcePrefab, Vector3.zero, Quaternion.identity)
                .GetComponent<CriAtomSource>();
        }
    }
}
