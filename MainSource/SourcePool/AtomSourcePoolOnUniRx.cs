using UniRx.Toolkit;
using UnityEngine;

namespace AdxSePlayer.SourcePool
{
    public class AtomSourcePoolOnUniRx : IObjectPool
    {
        private ObjectPool<CriAtomSource> _originPool;
        
        public GameObject sourcePrefab { private get; set; }

        public AtomSourcePoolOnUniRx(GameObject sourcePrefab)
        {
            this.sourcePrefab = sourcePrefab;
            _originPool = new SourceUniRxPoolOrigin(sourcePrefab);
        }

        public CriAtomSource Rent()
        {
            return SetDefaultParameter(_originPool.Rent());
        }
        
        private CriAtomSource SetDefaultParameter(CriAtomSource source)
        {
            source.player.ResetParameters();
            return source;
        }

        public void Return(CriAtomSource target)
        {
            _originPool.Return(target);
        }
    }
}
