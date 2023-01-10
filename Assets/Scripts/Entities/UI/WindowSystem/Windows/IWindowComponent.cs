using System.Threading.Tasks;
using Core.Interfaces;
using UnityEngine.UIElements;

namespace Entities.UI.WindowSystem.Windows
{
    // since OnOpen and OnClose is handeled by Constructor and OnDestroy, find another usecases
    // TODO: Refactor entire WindowHandler system
    public interface IWindowComponent : IConstantStateComponent
    {
        public WindowComponentConfig WindowComponentConfig { get; }
        public VisualElement RootContainer { get; }
        public VisualElement HolderContainer { get; }
        public void InitWindow(VisualElement container);
        public Task OnOpen();
        public Task OnClose();
    }
}