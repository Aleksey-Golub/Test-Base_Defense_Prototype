using Assets.CodeBase.Logic;
using Assets.CodeBase.Logic.CharacterComponents;
using UnityEngine;

namespace Assets.CodeBase.Enemies
{
    public partial class EnemyController
    {
        private abstract class EnemyStateBase : StateBase<EnemyController>
        {
            protected Timer FindTargetTimer;
            protected EnemyStateBase(EnemyController controller) : base(controller)
            {
                FindTargetTimer = new Timer();
            }

            protected Vector3 VectorToTarget()
            {
                return Controller._target.Transform.position - Controller.transform.position;
            }

            protected bool TargetNotNullAndAlive()
            {
                return Controller._target != null && Controller._target.IsAlive;
            }
        }
    }
}