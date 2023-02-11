using System.Threading.Tasks;
using Wooff.ECS.Context;

namespace Wooff.ECS.System
{
    public class System<T> : UpdateableContext<T> , ISystem<T> where T : IUpdateable
    {
        private int _shiftOneThread;
        public virtual void UpdateOneThread(float timeScale, IContext<T> data)
        {
            var chunk = data.SplitIntoChunks(1000);
            if (_shiftOneThread < chunk.Count)
            {
                foreach (var item in chunk[_shiftOneThread])
                    SystemUpdate(1f, item);
                _shiftOneThread++;
            }
            else
                _shiftOneThread = 0;
            
        }

        private int _shiftParallel;
        public virtual async Task UpdateParallelAsync(float timeScale, IContext<T> data)
        {
            var chunk = data.SplitIntoChunks(10);
            if (_shiftParallel < chunk.Count)
            {
                await chunk[_shiftParallel].ParallelForEachAsync(async item =>
                {
                    await SystemUpdateAsync(timeScale, item);
                });
                _shiftParallel++;
            }
            else
                _shiftParallel = 0;
        }
        
        protected virtual Task SystemUpdateAsync(float timeScale, T updateItem)
        {
            return Task.WhenAll();
        }
        
        protected virtual void SystemUpdate(float timeScale, T updateItem)
        {
        }
    }
}