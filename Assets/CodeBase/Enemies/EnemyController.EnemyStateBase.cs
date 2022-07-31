using Assets.CodeBase.Logic.CharacterComponents;

namespace Assets.CodeBase.Enemies
{
    public partial class EnemyController
    {
        private abstract class EnemyStateBase : StateBase<EnemyController>
        {
            protected EnemyStateBase(EnemyController controller)
            {
                Controller = controller;
            }
        }
    }
}