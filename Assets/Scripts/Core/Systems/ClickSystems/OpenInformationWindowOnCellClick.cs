using System.Linq;
using Core.Components;
using Core.Components.CellRelated;
using Core.Components.Tags;
using Core.Components.Tags.UiTags.Windows;
using Core.Components.TransformRelated;
using Core.Components.UnityRelated;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems.ClickSystems
{
    public class OpenInformationWindowOnCellClick : HandleClickedState<CellTagComponent>
    {
        protected override void ProcessClickedEntity(EntityContext context, IEntity clickedEntity)
        {
            if (GameStateManager.GetUiState != UiState.Information)
                return;
            
            var informationWindow = context.ContextGetAllFromMap(typeof(InformationWindowTagComponent)).FirstOrDefault();
            
            if(informationWindow is null)
                context.ContextAdd(new InformationWindowTagComponent(clickedEntity.ContextGet<InformationComponent>(), UiComponentsDataPrefabsHandler.InformationTagComponentData).CreateWindowEntityContainer());
            else
                informationWindow.ContextGet<InformationWindowTagComponent>().WindowComponent.UpdateTextInformation(clickedEntity.ContextGet<InformationComponent>());
        }
    }
}