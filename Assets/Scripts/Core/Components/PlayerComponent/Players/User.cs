using Wooff.ECS;
using Wooff.MonoIntegration;

namespace Core.Components.PlayerComponent.Players
{
    public class User : Player
    {
        public override PlayerType PlayerType => PlayerType.User;

        public User(IConfig data, IMonoEntity handler) : base(data, handler)
        {
        }
    }
}