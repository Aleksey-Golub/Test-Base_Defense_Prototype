using System;

namespace Assets.CodeBase.Data
{
    [Serializable]
    public class LootData
    {
        public LootData()
        {
            BaseStorage = new LootStorage();
            PlayerStorage = new LootStorage();
        }

        public LootStorage BaseStorage { get; private set; }
        public LootStorage PlayerStorage { get; private set; }

        public Action Changed;

        public void CollectForPlayer(LootType type, int amount)
        {
            PlayerStorage.Collect(type, amount);
            Changed?.Invoke();
        }

        public void MoveAllFromPlayerToBase()
        {
            LootStorage.Move(from: PlayerStorage, to: BaseStorage);
            Changed?.Invoke();
        }
    }

    public enum LootType
    {
        None,
        Gold,
        Gem,
    }
}