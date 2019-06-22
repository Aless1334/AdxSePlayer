using UnityEngine;

namespace AdxSePlayer.SourcePool
{
    public interface IObjectPool
    {
        GameObject sourcePrefab { set; }

        CriAtomSource Rent();
        void Return(CriAtomSource target);
    }
}