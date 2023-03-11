using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class TagIconVisualisation : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> _spriteRenderers;
        private Camera _camera;
        
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            transform.LookAt(transform.position + _camera.transform.position);
        }

        public void SetColor(Color color)
        {
            foreach (var spriteRenderer in _spriteRenderers)
                spriteRenderer.color = color;
        }
    }
}
