using System;
using UnityEngine;
using Assets.CodeBase.Player.Gun;

namespace Assets.CodeBase.Logic.CharacterComponents
{
    public class CharacterViewer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private AnimatorState _animatorState;

        private readonly int _idleStateHash = Animator.StringToHash("DynIdle");
        private readonly int _deathStateHash = Animator.StringToHash("Death");
        private readonly int _runStateHash = Animator.StringToHash("Running");
        private readonly int _throwWalkStateHash = Animator.StringToHash("Throw");
        private readonly int _riffleWalkStateHash = Animator.StringToHash("RiffleWalk");
        private readonly int _riffleIdleStateHash = Animator.StringToHash("RiffleIdle");

        public void PlayMove() => PlayAnimation(AnimatorState.Move);
        public void PlayIdle() => PlayAnimation(AnimatorState.Idle);
        public void PlayDeath() => PlayAnimation(AnimatorState.Death);
        public void PlayThrow() => PlayAnimation(AnimatorState.Throw);
        public void PlayIdleWithGun(GunType type)
        {
            switch (type)
            {
                case GunType.Riffle:
                    PlayAnimation(AnimatorState.RiffleIdle);
                    break;
                case GunType.None:
                default:
                    throw new NotImplementedException();
            }
        }

        public void PlayMoveWithGun(GunType type)
        {
            switch (type)
            {
                case GunType.Riffle:
                    PlayAnimation(AnimatorState.RiffleMove);
                    break;
                case GunType.None:
                default:
                    throw new NotImplementedException();
            }
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