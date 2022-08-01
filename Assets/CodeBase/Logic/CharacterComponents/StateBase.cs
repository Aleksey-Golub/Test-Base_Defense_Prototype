using UnityEngine;

namespace Assets.CodeBase.Logic.CharacterComponents
{
    public abstract class StateBase<T> where T : MonoBehaviour, ICharacterController
    {
        protected T Controller;

        protected StateBase(T controller)
        {
            Controller = controller;
        }

        public abstract void Enter();
        public abstract void Execute(float deltaTime);
        public abstract void Exit();
        protected abstract bool CheckNeedAndDoTransitions();
    }
}