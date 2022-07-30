using UnityEngine;

namespace Assets.CodeBase.Player.Gun
{
    public class ProjectileGun : GunBase
    {
        [Header("ProjectileGun")]
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private float _shootStrength = 1f;

        public override void Shoot()
        {
            Projectile newProjectile = Instantiate(_projectilePrefab, ShootPoint.position, ShootPoint.rotation);
            newProjectile.Rigidbody.velocity = ShootPoint.forward * _shootStrength;
        }
    }
}