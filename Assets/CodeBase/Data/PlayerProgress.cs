using System;

namespace Assets.CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;

        public PlayerProgress()
        {
            WorldData = new WorldData();
        }
    }
}
