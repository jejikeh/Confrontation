using Wooff.MonoIntegration;

namespace Core.Components.PlayerComponent.Players
{
    public class User : Player
    {
        public User(PlayerConfig data, IMonoEntity handler) : base(data, handler)
        {
        }
    }
}