using System.Collections.Generic;
using System.Globalization;
using Core.Components.Metrics;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class TagIconVisualisation : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> _spriteRenderers;
        [SerializeField] private TextMesh _unitsCount;
        [SerializeField] private TextMesh _protectionCount;
        [SerializeField] private TextMesh _attackCount;
        [CanBeNull] private MetricHandlerBalanceComponent _metricHandlerBalance;
        private Camera _camera;
        
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            transform.LookAt(transform.position + _camera.transform.position);
            if (_metricHandlerBalance is not null)
            {
                _unitsCount.text = _metricHandlerBalance.Balance[MetricType.Units]
                    .ToString(CultureInfo.InvariantCulture);
                _protectionCount.text = _metricHandlerBalance.Balance[MetricType.Protection]
                    .ToString(CultureInfo.InvariantCulture);
                _attackCount.text = _metricHandlerBalance.Balance[MetricType.Attack]
                    .ToString(CultureInfo.InvariantCulture);
            }
        }

        public void SetProperties(Color color, MetricHandlerBalanceComponent metricHandlerBalanceComponent)
        {
            _metricHandlerBalance = metricHandlerBalanceComponent;
            foreach (var spriteRenderer in _spriteRenderers)
                spriteRenderer.color = color;
        }
    }
}
