using Assets.CodeBase.Logic;
using Assets.CodeBase.Logic.CharacterComponents;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController
    {
        private class InBattleState : StateBase<PlayerController>
        {
            private Timer _findTargetTimer;

            public InBattleState(PlayerController player) : base(player)
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
                _findTargetTimer.Take(deltaTime);
                if (_findTargetTimer.Value >= Controller._targetFindDelay)
                {
                    _findTargetTimer.Reset();
                    Controller.FindTarget();
                }

                if (CheckNeedAndDoTransitions())
                    return;

                Vector3 normalizedMovementVector = new Vector3(Controller._input.Axis.x, 0, Controller._input.Axis.y).normalized;

                if (normalizedMovementVector != Vector3.zero)
                {
                    Controller._mover.Move(normalizedMovementVector, deltaTime);
                    Controller._viewer.PlayMoveWithGun(Controller._gun.Type);
                }
                else
                {
                    Controller._viewer.PlayIdleWithGun(Controller._gun.Type);
                }

                Vector3 direction = Controller._target.Transform.position - Controller.transform.position;
                Controller._rotator.RotateIn(direction.normalized, deltaTime);

                if (Controller._gun.CanAttack)
                    Controller._gun.Attack();
            }

            public override void Exit()
            {
                Controller._gun.Off();
                Controller._hPBar.SetState(false);
                _findTargetTimer.Reset();
            }

            protected override bool CheckNeedAndDoTransitions()
            {
                if (Controller._target == null || Controller._target.IsAlive == false)
                {
                    Controller.TransitionTo(PlayerState.OnLevel);
                    return true;
                }
                return false;
            }
        }
    }
}