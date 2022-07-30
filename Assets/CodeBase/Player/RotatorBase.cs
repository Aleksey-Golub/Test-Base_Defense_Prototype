using UnityEngine;

namespace Assets.CodeBase.Player
{
    public abstract class RotatorBase : MonoBehaviour
    {
        [SerializeField] protected float RotationSpeed = 360f;

        public abstract void RotateIn(Vector3 direction, float deltaTime);
    }
}