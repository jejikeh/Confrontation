using Core.Components.HelloWorldComponent;
using Wooff.ECS.Entity;
using Wooff.Presentation;

namespace Core.Entities
{
    public class Bob : Entity<IMonoComponent>
    {
        public Bob()
        {
            Add<HelloWorld, HelloWorldData>(new HelloWorldData()
            {
                Message = "BOB!"
            });
        }
    }
}