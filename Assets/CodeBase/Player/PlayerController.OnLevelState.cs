using Assets.CodeBase.Logic;
using Assets.CodeBase.Logic.CharacterComponents;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController
    {
        private class OnLevelState : StateBase<PlayerController>
        {
            private Timer _findTargetTimer;

            public OnLevelState(PlayerController player) : base(player)
            {
                _findTargetTimer = new Timer();
            }

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

                _findTargetTimer.Take(deltaTime);
                if (_findTargetTimer.Value >= Controller._targetFindDelay)
                {
                    _findTargetTimer.Reset();
                    Controller.FindTarget();
                }

                CheckNeedAndDoTransitions();
            }

            public override void Exit()
            {
                _findTargetTimer.Reset();
                Controller._gun.Off();
                Controller._hPBar.SetState(false);
            }

            protected override bool CheckNeedAndDoTransitions()
            {
                if (Controller._target != null && Controller._target.IsAlive)
                {
                    Controller.TransitionTo(PlayerState.InBattle);
                    return true;
                }
                return false;
            }
        }
    }
}