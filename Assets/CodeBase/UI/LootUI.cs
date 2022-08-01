using Assets.CodeBase.Data;
using TMPro;
using UnityEngine;

namespace Assets.CodeBase.UI
{
    public class LootUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gold;
        [SerializeField] private TextMeshProUGUI _gems;

        private LootData _lootData;

        public void Construct(LootData lootData)
        {
            _lootData = lootData;
            _lootData.Changed += OnLootDataChanged;

            OnLootDataChanged();
        }

        private void OnLootDataChanged()
        {
            _gold.text = $"{_lootData.BaseStorage.Gold}";
            _gems.text = $"{_lootData.BaseStorage.Gems}";
        }
    }
}