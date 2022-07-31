using Assets.CodeBase.Enemies.Loot;
using Assets.CodeBase.Logic;
using Assets.CodeBase.Logic.CharacterComponents;
using System;
using UnityEngine;

namespace Assets.CodeBase.Enemies
{
    public partial class EnemyController : MonoBehaviour, IDamageable, ICharacterController
    {
        [Header("References")]
        [SerializeField] private LootSpawner _lootSpawner;
        [SerializeField] private MoverBase _mover;
        [SerializeField] private RotatorBase _rotator;
        [SerializeField] private CharacterViewer _viewer;
        [SerializeField] private TargetFinderBase _targetFinder;

        [Header("Settings")]
        [SerializeField] private float _targetFindDelay = 1f;
        [SerializeField] private int _maxHP = 5;

        private IDamageable _target;
        private EnemyStateBase _state;

        public Transform Transform => transform;
        public bool IsAlive => HP > 0;
        public int HP { get; private set; }
        
        public event Action<int, int> HPChanged;

        private void Start()
        {
            Construct();
        }

        private void Update()
        {
            _state.Execute(Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _targetFinder.Radius);
        }

        public void Construct()
        {
            HP = _maxHP;
            HPChanged?.Invoke(HP, _maxHP);

            _state = new NoPlayerState(this);
            _state.Enter();
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            HP = HP < 0 ? 0 : HP;

            HPChanged?.Invoke(HP, _maxHP);

            if (HP == 0)
                Die();
        }

        private void TransitionTo(EnemyState transitionToState)
        {
            _state.Exit();
            _state = transitionToState switch
            {
                EnemyState.NoPlayer => new NoPlayerState(this),
                EnemyState.SeePlayer => new SeePlayer(this),
                EnemyState.None => throw new NotImplementedException(),
                //PlayerState.OnBase => new OnBaseState(this),
                //PlayerState.InBattle => new InBattleState(this),
                //PlayerState.OnLevel => new OnLevelState(this),
                _ => throw new NotImplementedException(),
            };

            _state.Enter();
        }

        private void Die()
        {
            _lootSpawner.Spawn();
            Destroy(gameObject);
        }

        private void FindTarget()
        {
            _target = _targetFinder.GetNearestTarget(transform.position);
        }

        public enum EnemyState
        {
            None,
            NoPlayer,
            SeePlayer,
        }
    }
}