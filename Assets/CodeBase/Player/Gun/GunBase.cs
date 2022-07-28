using UnityEngine;

namespace Assets.CodeBase.Player.Gun
{
    public abstract class GunBase : MonoBehaviour
    {
        [SerializeField] protected Transform ShootPoint;

        [field: SerializeField] public float ShootDelay { get; private set; } = 1f;

        public abstract void Shoot();
    }
}