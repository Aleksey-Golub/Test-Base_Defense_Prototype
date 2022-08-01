using Assets.CodeBase.Logic.CharacterComponents;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController
    {
        private class DeathState : StateBase<PlayerController>
        {
            public DeathState(PlayerController controller) : base(controller)
            { }

            public override void Enter()
            {
                Controller._hPBar.SetState(false);
                Controller.CanBeTarget = false;
                Controller._viewer.PlayDeath();
            }

            public override void Execute(float deltaTime)
            {
            }

            public override void Exit()
            {
                Controller._hPBar.SetState(true);
                Controller.CanBeTarget = true;
            }

            protected override bool CheckNeedAndDoTransitions() => false;
        }
    }
}