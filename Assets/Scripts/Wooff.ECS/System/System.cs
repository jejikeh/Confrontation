using System.Threading.Tasks;
using Wooff.ECS.Context;
using Wooff.ECS.Entity;

namespace Wooff.ECS.System
{
    public class System : ISystem
    {
        public virtual void Update(float timeScale, IContext<IEntity> data) { }
        
        public virtual Task UpdateParallelAsync(float timeScale, IContext<IEntity> data)
        {
            return Task.CompletedTask;
        }

        public void Update(float timeScale) { }

        public Task UpdateParallelAsync(float timeScale)
        {
            return Task.CompletedTask;
        }
    }
}