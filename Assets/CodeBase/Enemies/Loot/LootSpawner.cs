using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.CodeBase.Enemies.Loot
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private LootSettings[] _lootData;

        public void Spawn()
        {
            foreach (var data in _lootData)
            {
                if (data.SpawnChance == 0)
                    continue;

                if (data.SpawnChance - Random.Range(0, 100) >= 0)
                {
                    int amount = Random.Range(data.MinAmount, data.MaxAmount + 1);
                    Spawn(data.Prefab, amount);
                }
            }
        }

        private void Spawn(LootPiece prefab, int amount)
        {
            LootPiece lootPiece = Instantiate(prefab, transform.position, Quaternion.identity);
            lootPiece.Construct(amount);
        }
    }
}