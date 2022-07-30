using UnityEngine;

namespace Assets.CodeBase.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class MoverCharacterController : MoverBase
    {
        
        private CharacterController _characterController;

        public override bool IsMoved => _characterController.velocity.sqrMagnitude > Constants.Epsilon;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public override void Move(Vector3 input, float deltaTime)
        {
            _characterController.Move(MovementSpeed * deltaTime * input);
        }
    }
}