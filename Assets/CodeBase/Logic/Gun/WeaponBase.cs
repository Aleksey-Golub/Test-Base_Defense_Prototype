using UnityEngine;

namespace Assets.CodeBase.Logic.Gun
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [Header(nameof(WeaponBase))]
        [SerializeField] protected Transform ShootPoint;

        private Timer _rechargeTimer = new Timer();

        [field: SerializeField] public GunType Type { get; private set; }
        [field: SerializeField] public float AttackDelay { get; private set; } = 1f;

        public bool CanAttack => _rechargeTimer.Value >= AttackDelay;

        public void Attack()
        {
            _rechargeTimer.Reset();
            AttackIntarnal();
        }

        protected abstract void AttackIntarnal();
        public virtual void Off() => gameObject.SetActive(false);
        public virtual void On()
        {
            gameObject.SetActive(true);
        }

        public void Tick(float deltaTime) => _rechargeTimer.Take(deltaTime);
    }

    public enum GunType
    {
        None,
        Riffle,
        Hand,
    }
}