using UnityEngine;

namespace Assets.CodeBase.Player.Gun
{
    public class ProjectileGun : GunBase
    {
        [SerializeField] private Projectile _projectilePrefab;

        public override void Shoot()
        {
            Instantiate(_projectilePrefab, ShootPoint.position, ShootPoint.rotation);
        }
    }
}