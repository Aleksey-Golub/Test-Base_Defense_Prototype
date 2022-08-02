using Assets.CodeBase.Data;
using Assets.CodeBase.Enemies.Loot;
using Assets.CodeBase.Logic;
using Assets.CodeBase.Logic.CharacterComponents;
using Assets.CodeBase.Logic.Gun;
using Assets.CodeBase.Services.Input;
using Assets.CodeBase.UI;
using System;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController : CharacterControllerBase, IDamageable
    {
        [Header("References")]
        [SerializeField] private MoverBase _mover;
        [SerializeField] private RotatorBase _rotator;
        [SerializeField] private CharacterViewer _viewer;
        [SerializeField] private ProjectileGun _gun;
        [SerializeField] private HPBar _hPBar;

        [Header("Settings")]
        [SerializeField] private int _maxHP = 5;

        private IInputService _input;
        private PlayerStateBase _state;
        private PlayerProgress _progress;

        public Transform Transform => transform;
        public bool IsAlive => HP > 0;
        public bool CanBeTarget { get; private set; }

        [field: SerializeField] public int HP { get; private set; }

        public event Action<int, int> HPChanged;
        public event Action<IDamageable> Died;

        public void Update()
        {
            _gun.Tick(Time.deltaTime);

            _state.Execute(Time.deltaTime);
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

            Restart();
        }

        public void TransitionTo(PlayerState transitionToState)
        {
            _state?.Exit();
            _state = transitionToState switch
            {
                PlayerState.OnBase => new OnBaseState(this),
                PlayerState.OnLevel => new OnLevelState(this),
                PlayerState.InBattle => new InBattleState(this),
                PlayerState.Dead => new DeathState(this),
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

        public void Restart()
        {
            HP = _maxHP;
            HPChanged?.Invoke(HP, _maxHP);
            TransitionTo(PlayerState.OnBase);
        }

        private void Die()
        {
            TransitionTo(PlayerState.Dead);
            
            Died?.Invoke(this);
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
            Dead,
        }
    }
}