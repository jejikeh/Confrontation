using Core.Components.Tags.UiTags;
using UnityEngine;

namespace Core
{
    public class UiComponentsDataPrefabsHandler : Singleton<UiComponentsDataPrefabsHandler>
    {
        [SerializeField] 
        private WindowTagComponentData _informationTagComponentData;
        [SerializeField] 
        private WindowTagComponentData _toolBoxTagComponentData;
        [SerializeField] 
        private WindowTagComponentData _chooseCellTagComponentData;
        [SerializeField] 
        private WindowTagComponentData _metricShowerTagComponentData;
        
        public static WindowTagComponentData InformationTagComponentData => Instance._informationTagComponentData;
        public static WindowTagComponentData ToolBoxTagComponentData => Instance._toolBoxTagComponentData;
        public static WindowTagComponentData ChooseCellTagComponentData => Instance._chooseCellTagComponentData;
        public static WindowTagComponentData MetricShowerTagComponentData => Instance._metricShowerTagComponentData;
    }
}