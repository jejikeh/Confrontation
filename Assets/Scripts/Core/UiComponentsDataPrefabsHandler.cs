using Core.Components.Tags.UiTags;
using UnityEngine;

namespace Core.Components
{
    public class UiComponentsDataPrefabsHandler : Singleton<UiComponentsDataPrefabsHandler>
    {
        [SerializeField] 
        private InformationTagComponentData _informationTagComponentData;

        public static InformationTagComponentData InformationTagComponentData => Instance._informationTagComponentData;
    }
}