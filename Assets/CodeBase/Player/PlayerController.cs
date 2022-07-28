using Assets.CodeBase.Logic;
using Assets.CodeBase.Player.Gun;
using Assets.CodeBase.Services.Input;
using System;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private MoverBase _mover;
        [SerializeField] private PlayerViewer _viewer;
        [SerializeField] private PlayerTargetFinderBase _targetFinder;
        [SerializeField] private GunBase _gun;

        private IInputService _input;
        private PlayerStateBase _state;
        private IDamageable _target;

        public void Update()
        {
            _state.Execute(Time.deltaTime);
        }

        public void Construct(IInputService input)
        {
            _input = input;
            _state = new OnBaseState(this);
            _state.Enter();
        }

        internal void TransitionTo(PlayerState transitionToState)
        {
            _state.Exit();
            _state = transitionToState switch
            {
                PlayerState.OnBase => new OnBaseState(this),
                PlayerState.InBattle => new InBattleState(this),
                PlayerState.OnLevel => new OnLevelState(this),
                _ => throw new NotImplementedException(),
            };

            _state.Enter();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _targetFinder.Radius);
        }

        public abstract class PlayerStateBase
        {
            protected PlayerController Player;
            protected float Timer;

            protected PlayerStateBase(PlayerController player)
            {
                Player = player;
            }

            public abstract void Enter();
            public abstract void Execute(float deltaTime);
            public abstract void Exit();
            protected abstract void CheckTransitions();
        }

        public class OnBaseState : PlayerStateBase
        {
            public OnBaseState(PlayerController player) : base(player)
            { }

            public override void Enter()
            {
                Player._gun.gameObject.SetActive(false);
            }

            public override void Execute(float deltaTime)
            {
                Vector3 normalizedMovementVector = new Vector3(Player._input.Axis.x, 0, Player._input.Axis.y).normalized;

                if (normalizedMovementVector != Vector3.zero)
                {
                    Player._mover.Move(normalizedMovementVector);
                    Player._viewer.PlayMove();
                    Player.transform.rotation = Quaternion.LookRotation(normalizedMovementVector);
                }
                else
                {
                    Player._viewer.PlayIdle();
                }
            }
            public override void Exit()
            { }

            protected override void CheckTransitions()
            { }
        }

        public class OnLevelState : PlayerStateBase
        {
            private const float TARGET_FIND_DELAY = 1f;

            public OnLevelState(PlayerController player) : base(player)
            { }

            public override void Enter()
            {
                Player._gun.gameObject.SetActive(true);
            }

            public override void Execute(float deltaTime)
            {
                Vector3 normalizedMovementVector = new Vector3(Player._input.Axis.x, 0, Player._input.Axis.y).normalized;

                if (normalizedMovementVector != Vector3.zero)
                {
                    Player._mover.Move(normalizedMovementVector);
                    Player._viewer.PlayMoveWithRiffle();
                    Player.transform.rotation = Quaternion.LookRotation(normalizedMovementVector);
                }
                else
                {
                    Player._viewer.PlayIdleWithRiffle();
                }

                Timer += deltaTime;
                FindTarget();
                CheckTransitions();
            }

            public override void Exit()
            {
                Timer = 0;
            }

            protected override void CheckTransitions()
            {
                if (Player._target != null && Player._target.IsAlive)
                {
                    Player.TransitionTo(PlayerState.InBattle);
                    Exit();
                }
            }

            private void FindTarget()
            {
                if (Timer >= TARGET_FIND_DELAY)
                {
                    Timer -= TARGET_FIND_DELAY;
                    Player._target = Player._targetFinder.GetNearestTarget(Player.transform.position);
                }
            }
        }

        public class InBattleState : PlayerStateBase
        {
            public InBattleState(PlayerController player) : base(player)
            { }

            public override void Enter()
            {
                Player._gun.gameObject.SetActive(true);
            }

            public override void Execute(float deltaTime)
            {
                Vector3 normalizedMovementVector = new Vector3(Player._input.Axis.x, 0, Player._input.Axis.y).normalized;

                if (normalizedMovementVector != Vector3.zero)
                {
                    Player._mover.Move(normalizedMovementVector);
                    Player._viewer.PlayMoveWithRiffle();
                    Player.transform.rotation = Quaternion.LookRotation(Player._target.Transform.position - Player.transform.position);
                }
                else
                {
                    Player._viewer.PlayIdleWithRiffle();
                }

                Timer += deltaTime;
                if (Timer >= Player._gun.ShootDelay)
                    Player._gun.Shoot();
            }

            public override void Exit()
            {
                Timer = 0;
            }

            protected override void CheckTransitions()
            {
                if (Player._target == null || Player._target.IsAlive == false)
                {
                    Player.TransitionTo(PlayerState.OnLevel);
                    Exit();
                }
            }
        }

        public enum PlayerState
        {
            None,
            OnBase,
            OnLevel,
            InBattle,
        }
    }
}