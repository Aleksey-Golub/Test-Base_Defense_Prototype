using Assets.CodeBase.Logic;
using Assets.CodeBase.Player.Gun;
using Assets.CodeBase.Services.Input;
using System;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MoverBase _mover;
        [SerializeField] private PlayerViewer _viewer;
        [SerializeField] private PlayerTargetFinderBase _targetFinder;
        [SerializeField] private GunBase _gun;

        [Header("Settings")]
        [SerializeField] private  float _targetFindDelay = 1f;

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

        public void TransitionTo(PlayerState transitionToState)
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
                Player._gun.Off();
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
            public OnLevelState(PlayerController player) : base(player)
            { }

            public override void Enter()
            {
                Player._gun.On();
            }

            public override void Execute(float deltaTime)
            {
                Vector3 normalizedMovementVector = new Vector3(Player._input.Axis.x, 0, Player._input.Axis.y).normalized;

                if (normalizedMovementVector != Vector3.zero)
                {
                    Player._mover.Move(normalizedMovementVector);
                    Player._viewer.PlayMoveWithGun(Player._gun.Type);
                    Player.transform.rotation = Quaternion.LookRotation(normalizedMovementVector);
                }
                else
                {
                    Player._viewer.PlayIdleWithGun(Player._gun.Type);
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
                if (Timer >= Player._targetFindDelay)
                {
                    Timer -= Player._targetFindDelay;
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
                Player._gun.On();
            }

            public override void Execute(float deltaTime)
            {
                Vector3 normalizedMovementVector = new Vector3(Player._input.Axis.x, 0, Player._input.Axis.y).normalized;

                if (normalizedMovementVector != Vector3.zero)
                {
                    Player._mover.Move(normalizedMovementVector);
                    Player._viewer.PlayMoveWithGun(Player._gun.Type);
                    Player.transform.rotation = Quaternion.LookRotation(Player._target.Transform.position - Player.transform.position);
                }
                else
                {
                    Player._viewer.PlayIdleWithGun(Player._gun.Type);
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