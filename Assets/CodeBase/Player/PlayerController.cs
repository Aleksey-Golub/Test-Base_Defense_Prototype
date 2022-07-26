using Assets.CodeBase.Services.Input;
using System;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController : MonoBehaviour
    {
        [SerializeField] private MoverBase _mover;
        [SerializeField] private PlayerViewer _viewer;

        private IInputService _input;
        private PlayerStateBase _state;

        public void Update()
        {
            _state.Execute();
        }

        public void Construct(IInputService input)
        {
            _input = input;
            _state = new OnBaseState(this);
        }

        internal void TransitionTo(PlayerState transitionToState)
        {
            switch (transitionToState)
            {
                case PlayerState.OnBase:
                    _state = new OnBaseState(this);
                    break;
                case PlayerState.InBattle:
                    _state = new InBattleState(this);
                    break;
                case PlayerState.None:
                default:
                    throw new NotImplementedException();
            }
        }

        public abstract class PlayerStateBase
        {
            protected PlayerController _player;

            protected PlayerStateBase(PlayerController player)
            {
                _player = player;
            }

            public abstract void Execute();
        }

        public class OnBaseState : PlayerStateBase
        {
            public OnBaseState(PlayerController player) : base(player)
            {
            }

            public override void Execute()
            {
                Vector3 normalizedMovementVector = new Vector3(_player._input.Axis.x, 0, _player._input.Axis.y).normalized;

                if (normalizedMovementVector != Vector3.zero)
                {
                    _player._mover.Move(normalizedMovementVector);
                    _player._viewer.PlayMove();
                    _player.transform.rotation = Quaternion.LookRotation(normalizedMovementVector);
                }
                else
                {
                    _player._viewer.PlayIdle();
                }
            }
        }

        public class InBattleState : PlayerStateBase
        {
            public InBattleState(PlayerController player) : base(player)
            {
            }

            public override void Execute()
            {
                Debug.Log("InBattle");
            }
        }

        public enum PlayerState
        {
            None,
            OnBase,
            InBattle
        }
    }
}