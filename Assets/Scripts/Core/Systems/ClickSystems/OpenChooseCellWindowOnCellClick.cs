using System.Linq;
using Core.Components;
using Core.Components.CellRelated;
using Core.Components.Tags;
using Core.Components.Tags.UiTags.Windows;
using Core.Components.TransformRelated;
using Core.Components.UiRelated.Windows.ChooseCell;
using Core.Components.UnityRelated;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems.ClickSystems
{
    public class OpenChooseCellWindowOnCellClick : HandleClickedState<CellTagComponent>
    {
        protected override void ProcessClickedEntity(EntityContext context, IEntity clickedEntity)
        {
            if (GameStateManager.GetUiState != UiState.Build)
                return;
            
            var chooseCellWindow = context.ContextGetAllFromMap(typeof(ChooseCellWindowTagComponent)).FirstOrDefault();
            
            if(chooseCellWindow is null)
                context.ContextAdd(
                    new ChooseCellWindowTagComponent(clickedEntity, context, UiComponentsDataPrefabsHandler.ChooseCellTagComponentData)
                        .CreateWindowEntityContainer());
            else
                chooseCellWindow.ContextGet<ChooseCellWindowComponent>().UpdateClickedCell(clickedEntity);
        }
    }
}