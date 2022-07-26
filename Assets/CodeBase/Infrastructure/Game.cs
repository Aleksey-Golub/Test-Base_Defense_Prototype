using Assets.CodeBase.Player;
using Assets.CodeBase.Services.Input;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private PlayerController _player;

        private IInputService _inputService;

        private void Start()
        {
            RegisterServices();

            ConstructPlayer();
        }

        private void ConstructPlayer()
        {
            _player.Construct(_inputService);
        }

        private void RegisterServices()
        {
            _inputService = GetInputService();
        }

        private IInputService GetInputService()
        {
            return new MobileInputService(_joystick);
        }
    }
}