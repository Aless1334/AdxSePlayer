using UniRx.Toolkit;
using UnityEngine;

namespace AdxSePlayer
{
    public class AtomSourcePoolOnUniRx : IObjectPool
    {
        private const float DefaultVolume = 1.0f;
        private const float DefaultPitch = 0.0f;
        
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
            source.volume = DefaultVolume;
            source.pitch = DefaultPitch;
            return source;
        }

        public void Return(CriAtomSource target)
        {
            _originPool.Return(target);
        }
    }
}
