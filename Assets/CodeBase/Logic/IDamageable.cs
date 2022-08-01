using System;
using UnityEngine;

namespace Assets.CodeBase.Logic
{
    public interface IDamageable
    {
        Transform Transform { get; }
        bool IsAlive { get; }
        bool CanBeTarget { get; }
        int HP { get; }
        event Action<int, int> HPChanged;

        void TakeDamage(int damage);
    }
}