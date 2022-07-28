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
        private readonly int _throwWalkStateHash = Animator.StringToHash("Throw");
        private readonly int _riffleWalkStateHash = Animator.StringToHash("RiffleWalk");
        private readonly int _riffleIdleStateHash = Animator.StringToHash("RiffleIdle");

        internal void PlayMove() => PlayAnimation(AnimatorState.Move);
        internal void PlayIdle() => PlayAnimation(AnimatorState.Idle);
        internal void PlayIdleWithRiffle() => PlayAnimation(AnimatorState.RiffleIdle);
        internal void PlayDeath() => PlayAnimation(AnimatorState.Death);
        internal void PlayMoveWithRiffle() => PlayAnimation(AnimatorState.RiffleMove);
        internal void PlayThrow() => PlayAnimation(AnimatorState.Throw);

        private void PlayAnimation(AnimatorState state)
        {
            if (_animatorState == state)
                return;

            _animator.SetTrigger(GetAnimHash(state));
            _animatorState = state;
        }

        private int GetAnimHash(AnimatorState state)
        {
            return state switch
            {
                AnimatorState.Move => _runStateHash,
                AnimatorState.Idle => _idleStateHash,
                AnimatorState.Death => _deathStateHash,
                AnimatorState.Throw => _throwWalkStateHash,
                AnimatorState.RiffleMove => _riffleWalkStateHash,
                AnimatorState.RiffleIdle => _riffleIdleStateHash,
                AnimatorState.None => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
        }

        private enum AnimatorState
        {
            None,
            Move,
            Idle,
            Death,
            Throw,
            RiffleMove,
            RiffleIdle
        }
    }
}