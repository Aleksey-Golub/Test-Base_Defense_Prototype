using UnityEngine;

namespace Assets.CodeBase.CameraLogic
{
    public class CameraParent : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private void LateUpdate()
        {
            transform.position = _target.position;
        }
    }
}