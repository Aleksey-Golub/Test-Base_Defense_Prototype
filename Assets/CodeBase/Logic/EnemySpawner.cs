using Assets.CodeBase.Enemies;
using UnityEngine;

namespace Assets.CodeBase.Logic
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private int _maxEnemy = 5;
        [SerializeField] private EnemyController _enemyPrefab;
        [SerializeField] private float _spawnDelay = 5f;
        [SerializeField] private float _radius = 1f;

        private Timer _timer = new Timer();
        private int _enemyCount;

        private void Update()
        {
            _timer.Take(Time.deltaTime);

            if (_enemyCount < _maxEnemy && _timer.Value >= _spawnDelay)
            {
                Vector3 pos = Random.insideUnitSphere * _radius;
                pos.y = transform.position.y;
                pos += transform.position;

                EnemyController newEnemy = Instantiate(_enemyPrefab, pos, Quaternion.identity);
                newEnemy.Construct();
                newEnemy.Died += OnEnemyDied;

                _timer.Reset();
                _enemyCount++;
            }
        }

        private void OnEnemyDied(IDamageable damageable)
        {
            damageable.Died -= OnEnemyDied;
            _enemyCount--;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}