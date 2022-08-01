using UnityEngine;

namespace Assets.CodeBase.Enemies
{
    public partial class EnemyController
    {
        private class IdleState : EnemyStateBase
        {
            public IdleState(EnemyController controller) : base(controller)
            { }

            public override void Enter()
            {
            }

            public override void Execute(float deltaTime)
            {
                Debug.Log($"{Controller.name}: IdleState Execute");

                FindTargetTimer.Take(deltaTime);
                if (FindTargetTimer.Value >= Controller._targetFindDelay)
                {
                    FindTargetTimer.Reset();
                    Controller.FindTarget();
                }

                Controller._viewer.PlayIdle();
                CheckNeedAndDoTransitions();
            }

            public override void Exit()
            {
                FindTargetTimer.Reset();
            }

            protected override bool CheckNeedAndDoTransitions()
            {
                if (TargetNotNullAndAlive() && VectorToTarget().magnitude > Controller._attackDistance)
                {
                    Controller.TransitionTo(EnemyState.MoveToTarget);
                    return true;
                }
                else if (TargetNotNullAndAlive() && Controller._gun.CanAttack)
                {
                    Controller.TransitionTo(EnemyState.AttackTarget);
                    return true;
                }
                return false;
            }
        }
    }
}