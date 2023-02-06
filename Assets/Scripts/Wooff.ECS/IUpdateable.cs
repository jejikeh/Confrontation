using System.Threading.Tasks;

namespace Wooff.ECS
{
    public interface IUpdateable
    {
        public void Update(float timeScale);
        public Task UpdateParallelAsync(float timeScale);

        public Task UpdateTaskWrapper(float timeScale)
        {
            Update(timeScale);
            return Task.CompletedTask;
        }
    }

    public interface IUpdateable<in T> : IUpdateable
    {
        public void Update(float timeScale, T data);
        public Task UpdateParallelAsync(float timeScale, T data);
    }
}