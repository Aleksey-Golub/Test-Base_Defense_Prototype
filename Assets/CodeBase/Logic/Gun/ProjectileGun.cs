using UnityEngine;

namespace Assets.CodeBase.Logic.Gun
{
    public class ProjectileGun : WeaponBase
    {
        [Header(nameof(ProjectileGun))]
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private float _shootStrength = 1f;

        protected override void AttackIntarnal()
        {
            Projectile newProjectile = Instantiate(_projectilePrefab, ShootPoint.position, ShootPoint.rotation);
            newProjectile.Rigidbody.velocity = ShootPoint.forward * _shootStrength;
        }
    }
}