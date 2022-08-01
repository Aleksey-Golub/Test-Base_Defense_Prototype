using Assets.CodeBase.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase.UI.Windows
{
    public class RestartWindow : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;

        private IGameRestarted _game;

        public void Construct(IGameRestarted game)
        {
            SetState(false);

            _restartButton.onClick.AddListener(OnReastartClicked);
            _game = game;
        }

        public void SetState(bool state) => gameObject.SetActive(state);

        private void OnReastartClicked()
        {
            gameObject.SetActive(false);
            _game.RestartGame();
        }
    }
}
