using UnityEngine;

namespace Assets.CodeBase.Player
{
    public abstract class MoverBase : MonoBehaviour
    {
        [field: SerializeField] public float MovementSpeed { get; private set; } = 1f;

        public void Construct(float movementSpeed)
        {
            MovementSpeed = movementSpeed;
        }

        public abstract bool IsMoved { get; }

        public abstract void Move(Vector3 input, float deltaTime);
    }
}