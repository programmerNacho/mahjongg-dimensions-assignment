// Author: Ignacio María Muñoz Márquez

using UnityEngine;

namespace MahjonggDimensions
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private GameplayInput _gameplayInput;
        [SerializeField]
        private Transform _cameraTarget = null;
        [SerializeField]
        private float _rotationSpeed = 0f;

        private void Update()
        {
            float rotateInput = _gameplayInput.HorizontalInput;

            if (rotateInput != 0f)
            {
                RotateCamera(rotateInput * _rotationSpeed * Time.deltaTime);
            }
        }

        private void RotateCamera(float value)
        {
            _cameraTarget.Rotate(Vector3.up, value);
        }
    }
}
