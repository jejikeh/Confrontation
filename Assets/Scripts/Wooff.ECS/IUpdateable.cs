using System.Threading.Tasks;

namespace Wooff.ECS
{
    public interface IUpdateable
    {
        public void UpdateOneThread(float timeScale);
        public Task UpdateParallelAsync(float timeScale);
    }

    public interface IUpdateable<in T> : IUpdateable
    {
        public void UpdateOneThread(float timeScale, T data);
        public Task UpdateParallelAsync(float timeScale, T data);
    }
}