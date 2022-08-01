using UnityEngine;

namespace Assets.CodeBase.Logic.Gun
{
    public class MeleeWeapon : WeaponBase
    {
        [Header(nameof(MeleeWeapon))]
        [SerializeField] private float _radius = 0.5f;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private int _damage = 1;

        protected override void AttackIntarnal()
        {
            Collider[] colliders = Physics.OverlapSphere(ShootPoint.position, _radius, _layerMask);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(_damage);
                }
            }
        }
    }
}