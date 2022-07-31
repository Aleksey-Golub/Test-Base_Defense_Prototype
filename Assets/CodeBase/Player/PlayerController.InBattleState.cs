using Assets.CodeBase.Logic;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController
    {
        private class InBattleState : PlayerStateBase
        {
            private Timer _shootDelayTimer;
            private Timer _findTargetTimer;

            public InBattleState(PlayerController player) : base(player)
            {
                _shootDelayTimer = new Timer();
                _findTargetTimer = new Timer();
            }

            public override void Enter()
            {
                Controller._gun.On();
            }

            public override void Execute(float deltaTime)
            {
                Debug.Log("InBattle Execute");

                _findTargetTimer.Take(deltaTime);
                if (_findTargetTimer.Value >= Controller._targetFindDelay)
                {
                    _findTargetTimer.Take(-Controller._targetFindDelay);
                    Controller.FindTarget();
                }

                if (CheckAndDoTransitions())
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

                _shootDelayTimer.Take(deltaTime);
                if (_shootDelayTimer.Value >= Controller._gun.ShootDelay)
                {
                    _shootDelayTimer.Take(-Controller._gun.ShootDelay);
                    Controller._gun.Shoot();
                }
            }

            public override void Exit()
            {
                _shootDelayTimer.Reset();
            }

            protected override bool CheckAndDoTransitions()
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