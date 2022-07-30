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
                Player._gun.On();
            }

            public override void Execute(float deltaTime)
            {
                _findTargetTimer.Take(deltaTime);
                if (_findTargetTimer.Value >= Player._targetFindDelay)
                {
                    _findTargetTimer.Take(-Player._targetFindDelay);
                    Player.FindTarget();
                }

                if (CheckAndDoTransitions())
                    return;

                Vector3 normalizedMovementVector = new Vector3(Player._input.Axis.x, 0, Player._input.Axis.y).normalized;

                if (normalizedMovementVector != Vector3.zero)
                {
                    Player._mover.Move(normalizedMovementVector, deltaTime);
                    Player._viewer.PlayMoveWithGun(Player._gun.Type);
                }
                else
                {
                    Player._viewer.PlayIdleWithGun(Player._gun.Type);
                }

                Vector3 direction = Player._target.Transform.position - Player.transform.position;
                Player._rotator.RotateIn(direction.normalized, deltaTime);

                _shootDelayTimer.Take(deltaTime);
                if (_shootDelayTimer.Value >= Player._gun.ShootDelay)
                {
                    _shootDelayTimer.Take(-Player._gun.ShootDelay);
                    Player._gun.Shoot();
                }
            }

            public override void Exit()
            {
                _shootDelayTimer.Reset();
            }

            protected override bool CheckAndDoTransitions()
            {
                if (Player._target == null || Player._target.IsAlive == false)
                {
                    Player.TransitionTo(PlayerState.OnLevel);
                    return true;
                }
                return false;
            }
        }
    }
}