using Assets.CodeBase.Data;
using Assets.CodeBase.Player;
using Assets.CodeBase.Services.Input;
using Assets.CodeBase.Services.PersistentProgress;
using Assets.CodeBase.UI;
using Assets.CodeBase.UI.Windows;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public class Game : MonoBehaviour, IGameRestarted
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private PlayerController _player;
        [SerializeField] private Transform _playerStartPoint;
        [SerializeField] private LootUI _lootUI;
        [SerializeField] private RestartWindow _restartWindow;

        private IInputService _inputService;
        private IPersistentProgressService _progressService;

        private void Start()
        {
            RegisterServices();
            InitNewProgress();

            _player.Construct(_inputService, _progressService.Progress);
            _player.Died += (x) => _restartWindow.SetState(true);

            _lootUI.Construct(_progressService.Progress.WorldData.LootData);

            _restartWindow.Construct(this);
        }

        public void RestartGame()
        {
            _progressService.Progress.WorldData.LootData.PlayerStorage.Reset();

            _player.transform.position = _playerStartPoint.position;
            _player.Restart();
        }

        private void InitNewProgress()
        {
            _progressService.Progress = new PlayerProgress();
        }

        private void RegisterServices()
        {
            _inputService = GetInputService();
            _progressService = new PersistentProgressService();
        }

        private IInputService GetInputService()
        {
            return new MobileInputService(_joystick);
        }
    }
}