using UnityEngine;

namespace Assets.CodeBase.UI
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            transform.LookAt(_mainCamera.transform);
        }
    }
}
