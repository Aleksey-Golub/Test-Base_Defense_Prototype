using Assets.CodeBase.Logic;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController
    {
        private class OnLevelState : PlayerStateBase
        {
            private Timer _findTargetTimer;

            public OnLevelState(PlayerController player) : base(player)
            {
                _findTargetTimer = new Timer();
            }

            public override void Enter()
            {
                Player._gun.On();
                Player._hPBar.SetState(true);
            }

            public override void Execute(float deltaTime)
            {
                Debug.Log("OnLevel Execute");
                Vector3 normalizedMovementVector = new Vector3(Player._input.Axis.x, 0, Player._input.Axis.y).normalized;

                if (normalizedMovementVector != Vector3.zero)
                {
                    Player._mover.Move(normalizedMovementVector, deltaTime);
                    Player._viewer.PlayMoveWithGun(Player._gun.Type);
                    Player._rotator.RotateIn(normalizedMovementVector, deltaTime);
                }
                else
                {
                    Player._viewer.PlayIdleWithGun(Player._gun.Type);
                }

                _findTargetTimer.Take(deltaTime);
                if (_findTargetTimer.Value >= Player._targetFindDelay)
                {
                    _findTargetTimer.Take(-Player._targetFindDelay);
                    Player.FindTarget();
                }

                CheckAndDoTransitions();
            }

            public override void Exit()
            {
                _findTargetTimer.Reset();
            }

            protected override bool CheckAndDoTransitions()
            {
                if (Player._target != null && Player._target.IsAlive)
                {
                    Player.TransitionTo(PlayerState.InBattle);
                    return true;
                }
                return false;
            }
        }
    }
}