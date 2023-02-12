using System.Threading.Tasks;
using Wooff.ECS.Context;

namespace Wooff.ECS.System
{
    public class System<T> : ISystem<T> where T : IUpdateable
    {

        #region Start

        public virtual void StartOneThread()
        {
        }

        public virtual Task StartParallelAsync()
        {
            return Task.CompletedTask;
        }

        public virtual void StartOneThread(IContext<T> data)
        {
            foreach (var item in data)
                SystemStartOneThread(item);
        }

        protected virtual void SystemStartOneThread(T item)
        {
        }
        
        public virtual Task StartParallelAsync(IContext<T> data)
        {
            return Task.CompletedTask;
        }
        
        #endregion
        
        #region Update
        
        private int _shiftUpdateOneThread;
        public virtual void UpdateOneThread(float timeScale, IContext<T> data)
        {
            var chunk = data.SplitIntoChunks(1000);
            if (_shiftUpdateOneThread < chunk.Count)
            {
                foreach (var item in chunk[_shiftUpdateOneThread])
                    SystemUpdateOneThread(1f, item);
                _shiftUpdateOneThread++;
            }
            else
                _shiftUpdateOneThread = 0;
        }

        private int _shiftUpdateParallel;
        public virtual async Task UpdateParallelAsync(float timeScale, IContext<T> data)
        {
            var chunk = data.SplitIntoChunks(10);
            if (_shiftUpdateParallel < chunk.Count)
            {
                await chunk[_shiftUpdateParallel].ParallelForEachAsync(async item =>
                {
                    await SystemUpdateParallelAsync(timeScale, item);
                });
                _shiftUpdateParallel++;
            }
            else
                _shiftUpdateParallel = 0;
        }
        
        protected virtual Task SystemUpdateParallelAsync(float timeScale, T updateItem)
        {
            return Task.WhenAll();
        }
        
        protected virtual void SystemUpdateOneThread(float timeScale, T updateItem)
        {
        }
        
        #endregion

        public virtual void UpdateOneThread(float timeScale) { }

        public virtual Task UpdateParallelAsync(float timeScale) { return Task.CompletedTask; }
    }
}