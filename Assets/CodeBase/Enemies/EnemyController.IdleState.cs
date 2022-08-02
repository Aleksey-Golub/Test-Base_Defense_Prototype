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
                UpdateTimerAndTryFindTarget(deltaTime);

                Controller._viewer.PlayIdle();
                CheckNeedAndDoTransitions();
            }

            public override void Exit()
            {
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