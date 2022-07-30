using Assets.CodeBase.Logic;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public abstract class TargetFinderBase : MonoBehaviour
    {
        [field: SerializeField] public float Radius { get; private set; } = 5f;
        [field: SerializeField] public LayerMask LayerMask { get; private set; }

        public void Coustruct(float radius)
        {
            Radius = radius;
        }

        public abstract IDamageable GetNearestTarget(Vector3 point);
    }
}