using System.Threading.Tasks;
using Core;
using Entities.UI.ScreenSystem.Screens;
using UnityEngine.UIElements;

namespace Entities.UI.WindowSystem.Windows
{
    // TODO: for now window without config, but maybe use prefab of controlls as config
    public abstract class WindowComponent<T> : CustomComponent<T>, IWindowComponent where T : WindowComponentConfig
    {
        public WindowComponentConfig WindowComponentConfig => ComponentConfig;
        public VisualElement RootContainer { get; private set; }
        public VisualElement HolderContainer { get; private set; }

        private readonly UIDocument _uiDocument;

        protected VisualElement Container(string containerName)
        {
            return _uiDocument.rootVisualElement.Q<VisualElement>(containerName);
        }
        
        protected WindowComponent(IScreenComponent screenComponent, UIDocument uiDocument) : base(screenComponent.ScreenComponentConfig
            .FindWindowConfig<T>())
        {
            _uiDocument = uiDocument;
            RootContainer = RootContainer;
        }

        public abstract Task OnClose();
        public abstract Task OnOpen();
        
        public void InitWindow(VisualElement container)
        {
            RootContainer = ComponentConfig.WindowAsset.CloneTree();
            HolderContainer = container;
            container.Add(RootContainer);
        }
    }
}