using UnityEngine;

namespace Assets.CodeBase.Logic.CharacterComponents
{
    public abstract class CharacterStateBase<T> where T : CharacterControllerBase
    {
        protected T Controller;
        protected Timer FindTargetTimer;

        protected CharacterStateBase(T controller)
        {
            Controller = controller;
            FindTargetTimer = new Timer();
        }

        public abstract void Enter();
        public abstract void Execute(float deltaTime);
        public abstract void Exit();
        protected abstract bool CheckNeedAndDoTransitions();

        protected bool TargetNotNullAndAlive()
        {
            return Controller.Target != null && Controller.Target.IsAlive;
        }

        protected void UpdateTimerAndTryFindTarget(float deltaTime)
        {
            FindTargetTimer.Take(deltaTime);
            if (FindTargetTimer.Value >= Controller.TargetFindDelay)
            {
                FindTargetTimer.Reset();
                Controller.TryFindTarget();
            }
        }

        protected Vector3 VectorToTarget()
        {
            return Controller.Target.Transform.position - Controller.transform.position;
        }
    }
}