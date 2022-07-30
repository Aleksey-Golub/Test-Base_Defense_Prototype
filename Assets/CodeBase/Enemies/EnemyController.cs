using Assets.CodeBase.Enemies.Loot;
using Assets.CodeBase.Logic;
using System;
using UnityEngine;

namespace Assets.CodeBase.Enemies
{
    public class EnemyController : MonoBehaviour, IDamageable
    {
        [Header("References")]
        [SerializeField] private LootSpawner _lootSpawner;

        [Header("Settings")]
        [SerializeField] private int _maxHP = 5;

        public Transform Transform => transform;
        public bool IsAlive => HP > 0;
        public int HP { get; private set; }
        
        public event Action<int, int> HPChanged;

        private void Start()
        {
            Construct();
        }

        public void Construct()
        {
            HP = _maxHP;
            HPChanged?.Invoke(HP, _maxHP);
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            HP = HP < 0 ? 0 : HP;

            HPChanged?.Invoke(HP, _maxHP);

            if (HP == 0)
                Die();
        }

        private void Die()
        {
            _lootSpawner.Spawn();
            Destroy(gameObject);
        }
    }
}