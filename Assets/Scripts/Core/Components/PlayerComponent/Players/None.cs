using Wooff.ECS;
using Wooff.MonoIntegration;

namespace Core.Components.PlayerComponent.Players
{
    public class None : Player
    {
        public override PlayerType PlayerType => PlayerType.None;

        public None(IConfig data, IMonoEntity handler) : base(data, handler)
        {
        }
    }
}