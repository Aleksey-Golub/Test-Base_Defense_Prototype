using System;
using UnityEngine;

namespace Assets.CodeBase.Player.Gun
{
    public abstract class GunBase : MonoBehaviour
    {
        [Header("BaseGun")]
        [SerializeField] protected Transform ShootPoint;

        [field: SerializeField] public GunType Type { get; private set; }
        [field: SerializeField] public float ShootDelay { get; private set; } = 1f;

        public abstract void Shoot();

        public void Off() => gameObject.SetActive(false);

        public void On() => gameObject.SetActive(true);

    }

    public enum GunType
    {
        None,
        Riffle,
    }
}