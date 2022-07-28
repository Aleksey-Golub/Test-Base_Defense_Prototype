using UnityEngine;

namespace Assets.CodeBase.Logic
{
    public interface IDamageable
    {
        Transform Transform { get; }
        bool IsAlive { get; }
    }
}