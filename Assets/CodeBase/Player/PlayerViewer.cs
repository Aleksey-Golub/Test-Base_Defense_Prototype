using System;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public class PlayerViewer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private AnimatorState _animatorState;

        private readonly int _idleStateHash = Animator.StringToHash("DynIdle");
        private readonly int _deathStateHash = Animator.StringToHash("Death");
        private readonly int _runStateHash = Animator.StringToHash("Running");
        private readonly int _riffleWalkStateHash = Animator.StringToHash("RiffleWalk");
        private readonly int _throwWalkStateHash = Animator.StringToHash("Throw");

        internal void PlayMove()
        {
            PlayAnimation(AnimatorState.Move);
        }

        internal void PlayIdle()
        {
            PlayAnimation(AnimatorState.Idle);
        }

        internal void PlayDeath()
        {
            PlayAnimation(AnimatorState.Death);
        }

        private void PlayAnimation(AnimatorState state)
        {
            if (_animatorState == state)
                return;

            _animator.SetTrigger(GetAnimHash(state));
            _animatorState = state;
        }

        private int GetAnimHash(AnimatorState state)
        {
            switch (state)
            {
                case AnimatorState.Move:
                    return _runStateHash;
                case AnimatorState.Idle:
                    return _idleStateHash;
                case AnimatorState.Death:
                    return _deathStateHash;
                case AnimatorState.None:
                default:
                    throw new NotImplementedException();
            }
        }

        private enum AnimatorState
        {
            None,
            Move,
            Idle,
            Death
        }
    }
}