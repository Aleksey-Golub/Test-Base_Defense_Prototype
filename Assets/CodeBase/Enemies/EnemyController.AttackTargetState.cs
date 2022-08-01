using Assets.CodeBase.Logic;
using UnityEngine;

namespace Assets.CodeBase.Enemies
{
    public partial class EnemyController
    {
        private class AttackTargetState : EnemyStateBase
        {
            private Timer _timer;

            public AttackTargetState(EnemyController controller) : base(controller)
            {
                _timer = new Timer();
            }

            public override void Enter()
            {
                Controller._viewer.PlayThrow();
                Controller._viewer.AttackHappening += OnAttackHappening;
            }

            public override void Execute(float deltaTime)
            {
                Controller._rotator.RotateIn(VectorToTarget().normalized, deltaTime);

                _timer.Take(deltaTime);
                if (CheckNeedAndDoTransitions())
                    return;
            }

            public override void Exit()
            {
                Controller._viewer.AttackHappening -= OnAttackHappening;
            }

            protected override bool CheckNeedAndDoTransitions()
            {
                if (_timer.Value >= Controller._gun.AttackDelay)
                {
                    Controller.TransitionTo(EnemyState.Idle);
                    return true;
                }

                return false;
            }
            private void OnAttackHappening()
            {
                Controller._gun.Attack();
            }
        }
    }
}