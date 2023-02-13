using Wooff.ECS.Context;

namespace Wooff.ECS.System
{
    public class SystemContext<T> : UpdateableContext<ISystem<T>, IContext<T>>, IStartable<IContext<T>>
    {
        public void StartOneThread()
        {
            foreach (var system in this)
                system.StartOneThread();
        }
        
        public void StartOneThread(IContext<T> data)
        {
            foreach (var system in this)
                system.StartOneThread(data);
        }
    }
}