using System;

namespace Assets.CodeBase.Data
{
    [Serializable]
    public class LootStorage
    {
        public int Gold { get; private set; }
        public int Gems { get; private set; }

        public static void Move(LootStorage from, LootStorage to)
        {
            to.Gold += from.Gold;
            to.Gems += from.Gems;

            from.Reset();
        }

        public void Collect(LootType type, int amount)
        {
            switch (type)
            {
                case LootType.Gold:
                    Gold += amount;
                    break;
                case LootType.Gem:
                    Gems += amount;
                    break;
                case LootType.None:
                default:
                    throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return $"{nameof(Gold)}: {Gold}; {nameof(Gems)}: {Gems}";
        }

        private void Reset()
        {
            Gold = 0;
            Gems = 0;
        }
    }
}