using System.Threading.Tasks;
using NotImplementedException = System.NotImplementedException;

namespace Wooff.ECS.Context
{
    public class UpdateableContext<T> : Context<T>, IUpdateable where T : IUpdateable
    {
        public void Update(float timeScale)
        {
            foreach (var updateable in this)
                updateable.Update(timeScale);
        }

        public async Task UpdateParallelAsync(float timeScale)
        {
            foreach (var chunk in SplitIntoChunks(4))
                await chunk.ParallelForEachAsync(async updateable => await updateable.UpdateParallelAsync(timeScale));
        }
    }
    
    public class UpdateableContext<T, T1> : Context<T>, IUpdateable<T1> where T : IUpdateable<T1>
    {
        public void Update(float timeScale, T1 data)
        {
            foreach (var updateable in this)
                updateable.Update(timeScale, data);
        }

        public async Task UpdateParallelAsync(float timeScale, T1 data)
        {
            foreach (var chunk in SplitIntoChunks(4))
                await chunk.ParallelForEachAsync(async updateable => await updateable.UpdateParallelAsync(timeScale, data));
        }

        public void Update(float timeScale)
        {
            foreach (var updateable in this)
                updateable.Update(timeScale);
        }

        public async Task UpdateParallelAsync(float timeScale)
        {
            foreach (var chunk in SplitIntoChunks(4))
                await chunk.ParallelForEachAsync(async updateable => await updateable.UpdateParallelAsync(timeScale));
        }
    }
}