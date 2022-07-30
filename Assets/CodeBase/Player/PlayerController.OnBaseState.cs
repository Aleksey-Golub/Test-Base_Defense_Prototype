using UnityEngine;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController
    {

        private class OnBaseState : PlayerStateBase
        {
            public OnBaseState(PlayerController player) : base(player)
            { }

            public override void Enter()
            {
                Player.RemoveAllResourcesToBase();
                Player._gun.Off();
                Player._hPBar.SetState(false);
            }

            public override void Execute(float deltaTime)
            {
                Debug.Log("OnBase Execute");
                Vector3 normalizedMovementVector = new Vector3(Player._input.Axis.x, 0, Player._input.Axis.y).normalized;

                if (normalizedMovementVector != Vector3.zero)
                {
                    Player._mover.Move(normalizedMovementVector, deltaTime);
                    Player._viewer.PlayMove();
                    Player._rotator.RotateIn(normalizedMovementVector, deltaTime);
                }
                else
                {
                    Player._viewer.PlayIdle();
                }
            }

            public override void Exit()
            { }

            protected override bool CheckAndDoTransitions() => false;
        }
    }
}