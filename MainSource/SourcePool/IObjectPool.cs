using UnityEngine;

namespace AdxSePlayer.MainSource.SourcePool
{
    public interface IObjectPool
    {
        GameObject sourcePrefab { set; }

        CriAtomSource Rent();
        void Return(CriAtomSource target);
    }
}