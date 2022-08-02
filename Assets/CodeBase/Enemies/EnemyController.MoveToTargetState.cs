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
                UpdateTimerAndTryFindTarget(deltaTime);

                if (CheckNeedAndDoTransitions())
                    return;

                Vector3 normalizedMovementVector = (Controller.Target.Transform.position - Controller.transform.position).normalized;
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
            }

            protected override bool CheckNeedAndDoTransitions()
            {
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