using System;

namespace Assets.CodeBase.Enemies
{
    public partial class EnemyController
    {
        private class SeePlayer : EnemyStateBase
        {
            public SeePlayer(EnemyController controller) : base(controller)
            {
            }

            public override void Enter()
            {
                throw new NotImplementedException();
            }

            public override void Execute(float deltaTime)
            {
                throw new NotImplementedException();
            }

            public override void Exit()
            {
                throw new NotImplementedException();
            }

            protected override bool CheckAndDoTransitions()
            {
                throw new NotImplementedException();
            }
        }
    }
}