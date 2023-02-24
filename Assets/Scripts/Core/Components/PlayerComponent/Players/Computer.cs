using Wooff.MonoIntegration;

namespace Core.Components.PlayerComponent.Players
{
    public class Computer : Player
    {
        public Computer(PlayerConfig data, IMonoEntity handler) : base(data, handler)
        {
        }
    }
}