using UnityEngine;

namespace Assets.CodeBase.Logic.CharacterComponents
{
    [RequireComponent(typeof(CharacterController))]
    public class MoverCharacterController : MoverBase
    {
        [SerializeField] private CharacterController _characterController;

        public override bool IsMoved => _characterController.velocity.sqrMagnitude > Constants.Epsilon;

        public override void Move(Vector3 input, float deltaTime)
        {
            _characterController.Move(MovementSpeed * deltaTime * input);
        }
    }
}