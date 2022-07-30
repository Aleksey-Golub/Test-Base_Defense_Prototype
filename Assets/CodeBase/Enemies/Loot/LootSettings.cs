using System;
using UnityEngine;

namespace Assets.CodeBase.Enemies.Loot
{
    [Serializable]
    public class LootSettings
    {
        [SerializeField] private LootPiece _prefab;
        [SerializeField, Range(0, 100)] private float _spawnChance = 50f;
        [SerializeField, Min(1)] private int _minAmount = 1;
        [SerializeField, Min(1)] private int _maxAmount = 10;

        public LootPiece Prefab => _prefab;
        public float SpawnChance => _spawnChance;
        public int MinAmount => _minAmount;
        public int MaxAmount => _maxAmount;
    }
}