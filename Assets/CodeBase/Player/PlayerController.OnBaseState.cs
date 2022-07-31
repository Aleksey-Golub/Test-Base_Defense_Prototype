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
                Controller.RemoveAllResourcesToBase();
                Controller._gun.Off();
                Controller._hPBar.SetState(false);
            }

            public override void Execute(float deltaTime)
            {
                Debug.Log("OnBase Execute");
                Vector3 normalizedMovementVector = new Vector3(Controller._input.Axis.x, 0, Controller._input.Axis.y).normalized;

                if (normalizedMovementVector != Vector3.zero)
                {
                    Controller._mover.Move(normalizedMovementVector, deltaTime);
                    Controller._viewer.PlayMove();
                    Controller._rotator.RotateIn(normalizedMovementVector, deltaTime);
                }
                else
                {
                    Controller._viewer.PlayIdle();
                }
            }

            public override void Exit()
            { }

            protected override bool CheckAndDoTransitions() => false;
        }
    }
}