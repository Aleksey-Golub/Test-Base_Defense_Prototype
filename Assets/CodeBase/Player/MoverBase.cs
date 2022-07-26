using UnityEngine;

namespace Assets.CodeBase.Player
{
    public abstract class MoverBase : MonoBehaviour
    {
        [SerializeField] protected float MovementSpeed = 1f;

        public abstract bool IsMoved { get; }

        public abstract void Move(Vector3 input);
    }
}