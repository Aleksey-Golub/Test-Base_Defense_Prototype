using UnityEngine;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController
    {
        private class OnLevelState : PlayerStateBase
        {
            public OnLevelState(PlayerController player) : base(player)
            { }

            public override void Enter()
            {
                Controller._gun.On();
                Controller._hPBar.SetState(true);
            }

            public override void Execute(float deltaTime)
            {
                Vector3 normalizedMovementVector = new Vector3(Controller._input.Axis.x, 0, Controller._input.Axis.y).normalized;

                if (normalizedMovementVector != Vector3.zero)
                {
                    Controller._mover.Move(normalizedMovementVector, deltaTime);
                    Controller._viewer.PlayMoveWithGun(Controller._gun.Type);
                    Controller._rotator.RotateIn(normalizedMovementVector, deltaTime);
                }
                else
                {
                    Controller._viewer.PlayIdleWithGun(Controller._gun.Type);
                }

                UpdateTimerAndTryFindTarget(deltaTime);

                CheckNeedAndDoTransitions();
            }

            public override void Exit()
            {
                Controller._gun.Off();
                Controller._hPBar.SetState(false);
            }

            protected override bool CheckNeedAndDoTransitions()
            {
                if (Controller.Target != null && Controller.Target.IsAlive)
                {
                    Controller.TransitionTo(PlayerState.InBattle);
                    return true;
                }
                return false;
            }
        }
    }
}