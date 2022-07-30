using UnityEngine;

namespace Assets.CodeBase.Player
{
    public class TransformRotator : RotatorBase
    {
        public override void RotateIn(Vector3 direction, float deltaTime)
        {
            //transform.rotation = Quaternion.LookRotation(direction);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, RotationSpeed * deltaTime, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}