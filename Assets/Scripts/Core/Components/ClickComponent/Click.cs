using Core.Components.ClickableComponent;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.ClickComponent
{
    public class Click : Component<ClickConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>
    {
        public Click(ClickConfig data, IMonoEntity handler) : base(data, handler)
        {
        }

        public void SetActiveLayer(ClickLayer clickLayer)
        {
            Config.CurrentActiveLayer = clickLayer;
        }

        public void StartClick(Clickable clickable)
        {
            if (Config.CurrentActiveLayer != clickable.Config.ClickLayer) 
                return;
            
            clickable.ClickOnMe();
            Config.LastClickable = clickable;
        }
        
        IConfig IConfigurable<IConfig>.Config => Config;
    }
}