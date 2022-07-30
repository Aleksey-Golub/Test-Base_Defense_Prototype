using Assets.CodeBase.Logic;
using Assets.CodeBase.Player.Gun;
using Assets.CodeBase.Services.Input;
using System;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController : MonoBehaviour, IDamageable
    {
        [Header("References")]
        [SerializeField] private MoverBase _mover;
        [SerializeField] private RotatorBase _rotator;
        [SerializeField] private PlayerViewer _viewer;
        [SerializeField] private TargetFinderBase _targetFinder;
        [SerializeField] private GunBase _gun;

        [Header("Settings")]
        [SerializeField] private float _targetFindDelay = 1f;
        [SerializeField] private int _maxHP = 5;

        private IInputService _input;
        
        private IDamageable _target;
        private PlayerStateBase _state;


        public Transform Transform => transform;
        public bool IsAlive => HP > 0;
        public int HP { get; private set; }

        public void Update()
        {
            _state.Execute(Time.deltaTime);
        }

        public void Construct(IInputService input)
        {
            _input = input;
            HP = _maxHP;

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

        public void TakeDamage(int damage)
        {
            HP -= damage;
            HP = HP < 0 ? 0 : HP;

            if (HP == 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void FindTarget()
        {
            _target = _targetFinder.GetNearestTarget(transform.position);
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