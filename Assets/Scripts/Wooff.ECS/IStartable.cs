using System.Threading.Tasks;

namespace Wooff.ECS
{
    public interface IStartable
    {
        public void StartOneThread();
        public Task StartParallelAsync();
    }
    
    public interface IStartable<in T> : IStartable
    {
        public void StartOneThread(T data);
        public Task StartParallelAsync(T data);
    }
}