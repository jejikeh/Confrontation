using System.Threading.Tasks;
using Wooff.ECS.Context;
using NotImplementedException = System.NotImplementedException;

namespace Wooff.ECS.System
{
    public class SystemContext<T> : UpdateableContext<ISystem<T>, IContext<T>>, IStartable<IContext<T>>
    {
        public void StartOneThread()
        {
            foreach (var system in this)
                system.StartOneThread();
        }

        public async Task StartParallelAsync()
        {
            foreach (var system in this)
                await system.StartParallelAsync();
        }

        public void StartOneThread(IContext<T> data)
        {
            foreach (var system in this)
                system.StartOneThread(data);
        }

        public async Task StartParallelAsync(IContext<T> data)
        {
            foreach (var system in this)
                await system.StartParallelAsync(data);
        }
    }
}