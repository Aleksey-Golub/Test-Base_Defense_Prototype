using UnityEngine;

namespace Assets.CodeBase.Player
{
    public abstract class RotatorBase : MonoBehaviour
    {
        [SerializeField] protected float RotationSpeed = 1f;

        public abstract void RotateIn(Vector3 direction, float deltaTime);
    }
}