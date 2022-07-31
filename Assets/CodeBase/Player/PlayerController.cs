using Assets.CodeBase.Data;
using Assets.CodeBase.Enemies.Loot;
using Assets.CodeBase.Logic;
using Assets.CodeBase.Logic.CharacterComponents;
using Assets.CodeBase.Player.Gun;
using Assets.CodeBase.Services.Input;
using Assets.CodeBase.UI;
using System;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController : MonoBehaviour, IDamageable, ICharacterController
    {
        [Header("References")]
        [SerializeField] private MoverBase _mover;
        [SerializeField] private RotatorBase _rotator;
        [SerializeField] private CharacterViewer _viewer;
        [SerializeField] private TargetFinderBase _targetFinder;
        [SerializeField] private GunBase _gun;
        [SerializeField] private HPBar _hPBar;

        [Header("Settings")]
        [SerializeField] private float _targetFindDelay = 1f;
        [SerializeField] private int _maxHP = 5;

        private IInputService _input;
        private IDamageable _target;
        private PlayerStateBase _state;
        private PlayerProgress _progress;

        public Transform Transform => transform;
        public bool IsAlive => HP > 0;
        public int HP { get; private set; }
        
        public event Action<int, int> HPChanged;

        public void Update()
        {
            _state.Execute(Time.deltaTime);

            Debug.Log($"BaseStorage: {_progress.WorldData.LootData.BaseStorage}, \nPlayerStarage: {_progress.WorldData.LootData.PlayerStorage}");
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _targetFinder.Radius);
        }

        public void Construct(IInputService input, PlayerProgress progress)
        {
            _input = input;
            _progress = progress;
            _hPBar.Construct(this);

            HP = _maxHP;
            HPChanged?.Invoke(HP, _maxHP);

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
                PlayerState.None => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };

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

        public void Collect(LootPiece lootPiece)
        {
            _progress.WorldData.LootData.CollectForPlayer(lootPiece.Type, lootPiece.Amount);
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void FindTarget()
        {
            _target = _targetFinder.GetNearestTarget(transform.position);
        }

        private void RemoveAllResourcesToBase()
        {
            _progress.WorldData.LootData.MoveAllFromPlayerToBase();
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