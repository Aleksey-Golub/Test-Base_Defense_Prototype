using UnityEngine;

namespace Assets.CodeBase.Logic
{
    public interface IDamageable
    {
        Transform Transform { get; }
        bool IsAlive { get; }
        int HP { get; }

        void TakeDamage(int damage);
    }
}