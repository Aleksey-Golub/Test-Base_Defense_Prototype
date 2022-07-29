using Assets.CodeBase.Player;
using UnityEngine;
using static Assets.CodeBase.Player.PlayerController;

namespace Assets.CodeBase.Logic
{
    public class GateTrigger : MonoBehaviour
    {
        [SerializeField] private PlayerState _transitionToState;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                player.TransitionTo(_transitionToState);
            }
        }
    }
}