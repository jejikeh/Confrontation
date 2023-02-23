using Wooff.ECS;
using Wooff.MonoIntegration;

namespace Core.Components.PlayerComponent.Players
{
    public class Computer : Player
    {
        public override PlayerType PlayerType => PlayerType.None;

        public Computer(IConfig data, IMonoEntity handler) : base(data, handler)
        {
        }
    }
}