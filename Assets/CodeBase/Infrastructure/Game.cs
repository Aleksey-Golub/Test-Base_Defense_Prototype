using Assets.CodeBase.Data;
using Assets.CodeBase.Player;
using Assets.CodeBase.Services.Input;
using Assets.CodeBase.Services.PersistentProgress;
using Assets.CodeBase.UI;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private PlayerController _player;
        [SerializeField] private LootViewer _lootViewer;

        private IInputService _inputService;
        private IPersistentProgressService _progressService;

        private void Start()
        {
            RegisterServices();
            InitNewProgress();

            _player.Construct(_inputService, _progressService.Progress);
            _lootViewer.Construct(_progressService.Progress.WorldData.LootData);
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