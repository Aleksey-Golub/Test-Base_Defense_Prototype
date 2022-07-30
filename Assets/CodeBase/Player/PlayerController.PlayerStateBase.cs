namespace Assets.CodeBase.Player
{
    public partial class PlayerController
    {
        private abstract class PlayerStateBase
        {
            protected PlayerController Player;

            protected PlayerStateBase(PlayerController player)
            {
                Player = player;
            }

            public abstract void Enter();
            public abstract void Execute(float deltaTime);
            public abstract void Exit();
            protected abstract bool CheckAndDoTransitions();
        }
    }
}