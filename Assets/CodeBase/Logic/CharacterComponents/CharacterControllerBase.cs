using UnityEngine;

namespace Assets.CodeBase.Logic.CharacterComponents
{
    public abstract class CharacterControllerBase : MonoBehaviour
    {
        [SerializeField] protected TargetFinderBase _targetFinder;
        
        [field: SerializeField] public float TargetFindDelay { get; private set; } = 1f;

        public IDamageable Target { get; private set; }

        public bool TryFindTarget()
        {
            Target = _targetFinder.GetNearestTargetOrNull(transform.position);

            return Target != null;
        }
    }
}