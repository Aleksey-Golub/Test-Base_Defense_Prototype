using Assets.CodeBase.Logic;
using UnityEngine;

namespace Assets.CodeBase.Player.Gun
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private int _damage = 2;

        private bool _collisionHappened;

        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        private void OnCollisionEnter(Collision collision)
        {
            if (_collisionHappened)
                return;

            _collisionHappened = true;

            if (collision.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
                Instantiate(_particle, collision.GetContact(0).point, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}