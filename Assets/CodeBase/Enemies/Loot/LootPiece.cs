using Assets.CodeBase.Data;
using Assets.CodeBase.Player;
using UnityEngine;

namespace Assets.CodeBase.Enemies.Loot
{
    [RequireComponent(typeof(Rigidbody))]
    public class LootPiece : MonoBehaviour
    {
        [field: SerializeField] public LootType Type { get; private set; }

        private bool _collected;

        public int Amount { get; private set; }

        public void Construct(int amount)
        {
            Amount = amount;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_collected)
                return;

            if (other.TryGetComponent(out PlayerController player))
            {
                player.Collect(this);
                _collected = true;

                Destroy(gameObject);
            }
        }
    }
}