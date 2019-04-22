using UnityEngine;

namespace Nagasono.AudioScripts.ADX
{
    public interface IObjectPool
    {
        GameObject sourcePrefab { set; }

        CriAtomSource Rent();
        void Return(CriAtomSource target);
    }
}