using UnityEngine;

namespace Core
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _camera;
        void Start()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            transform.LookAt(transform.position + _camera.transform.position);
        }
    }
}
