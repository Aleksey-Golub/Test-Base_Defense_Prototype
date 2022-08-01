using UnityEngine;

namespace Assets.CodeBase.Enemies
{
    public partial class EnemyController
    {
        private class MoveToTargetState : EnemyStateBase
        {
            public MoveToTargetState(EnemyController controller) : base(controller)
            { }

            public override void Enter()
            { }

            public override void Execute(float deltaTime)
            {
                FindTargetTimer.Take(deltaTime);
                if (FindTargetTimer.Value >= Controller._targetFindDelay)
                {
                    FindTargetTimer.Reset();
                    Controller.FindTarget();
                }

                if (CheckNeedAndDoTransitions())
                    return;

                Vector3 normalizedMovementVector = (Controller._target.Transform.position - Controller.transform.position).normalized;
                if (normalizedMovementVector != Vector3.zero)
                {
                    Controller._mover.Move(normalizedMovementVector, deltaTime);
                    Controller._viewer.PlayRun();
                    Controller._rotator.RotateIn(normalizedMovementVector, deltaTime);
                }
                else
                {
                    Controller._viewer.PlayIdle();
                }
            }

            public override void Exit()
            {
                FindTargetTimer.Reset();
            }

            protected override bool CheckNeedAndDoTransitions()
            {
                //if (Controller._target == null || Controller._target.IsAlive == false)
                if (TargetNotNullAndAlive() == false)
                {
                    Controller.TransitionTo(EnemyState.Idle);
                    return true;
                }
                else if (VectorToTarget().magnitude <= Controller._attackDistance && Controller._gun.CanAttack)
                {
                    Controller.TransitionTo(EnemyState.AttackTarget);
                    return true;
                }
                return false;
            }
        }
    }
}