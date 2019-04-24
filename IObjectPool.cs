using UnityEngine;

namespace AdxSePlayer
{
    public interface IObjectPool
    {
        GameObject sourcePrefab { set; }

        CriAtomSource Rent();
        void Return(CriAtomSource target);
    }
}