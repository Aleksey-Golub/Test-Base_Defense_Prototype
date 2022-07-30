using Assets.CodeBase.Logic;
using UnityEngine;

namespace Assets.CodeBase.Enemies
{
    public class EnemyController : MonoBehaviour, IDamageable
    {
        [Header("References")]

        [Header("Settings")]
        [SerializeField] private int _maxHP = 5;

        public Transform Transform => transform;
        public bool IsAlive => HP > 0;
        public int HP { get; private set; }

        private void Start()
        {
            Construct();
        }

        public void Construct()
        {
            HP = _maxHP;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            HP = HP < 0 ? 0 : HP;

            if (HP == 0)
                Die();
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}