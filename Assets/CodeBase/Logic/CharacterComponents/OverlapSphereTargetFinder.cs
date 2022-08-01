using UnityEngine;

namespace Assets.CodeBase.Logic.CharacterComponents
{
    public class OverlapSphereTargetFinder : TargetFinderBase
    {
        public override IDamageable GetNearestTargetOrNull(Vector3 point)
        {
            Collider[] colliders = Physics.OverlapSphere(point, Radius, LayerMask);

            IDamageable nearest = null;
            float distance = float.MaxValue;
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IDamageable target) && target.CanBeTarget)
                {
                    float distanceToTarget = Vector3.SqrMagnitude(target.Transform.position - point);
                    if (distanceToTarget < distance)
                    {
                        distance = distanceToTarget;
                        nearest = target;
                    }
                }
            }

            return nearest;
        }
    }
}