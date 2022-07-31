using Assets.CodeBase.Logic;

namespace Assets.CodeBase.Enemies
{
    public partial class EnemyController
    {
        private class NoPlayerState : EnemyStateBase
        {
            private Timer _findTargetTimer;

            public NoPlayerState(EnemyController controller) : base(controller)
            {
                _findTargetTimer = new Timer();
            }

            public override void Enter()
            {
            }

            public override void Execute(float deltaTime)
            {
                _findTargetTimer.Take(deltaTime);
                if (_findTargetTimer.Value >= Controller._targetFindDelay)
                {
                    _findTargetTimer.Take(-Controller._targetFindDelay);
                    Controller.FindTarget();
                }

                CheckAndDoTransitions();
            }

            public override void Exit()
            {
                _findTargetTimer.Reset();
            }

            protected override bool CheckAndDoTransitions()
            {
                if (Controller._target != null && Controller._target.IsAlive)
                {
                    Controller.TransitionTo(EnemyState.SeePlayer);
                    return true;
                }
                return false;
            }
        }
    }
}