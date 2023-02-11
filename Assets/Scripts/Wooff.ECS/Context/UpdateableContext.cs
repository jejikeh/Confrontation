using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wooff.ECS.Context
{
    public class UpdateableContext<T> : Context<T>, IUpdateable where T : IUpdateable
    {
        public void UpdateOneThread(float timeScale)
        {
            foreach (var updateable in this.Where(x => x is not null))
                updateable.UpdateOneThread(timeScale);
        }

        public async Task UpdateParallelAsync(float timeScale)
        {
            await this.Where(x => x is not null).ParallelForEachAsync(async x =>
            {
                x.UpdateParallelAsync(timeScale).Start();
            });
        }
    }
    
    public class UpdateableContext<T, T1> : Context<T>, IUpdateable<T1> where T : IUpdateable<T1>
    {
        public void UpdateOneThread(float timeScale, T1 data)
        {
            foreach (var updateable in this)
                updateable?.UpdateOneThread(timeScale, data);
        }

        public async Task UpdateParallelAsync(float timeScale, T1 data)
        {
            await this.Where(x => x is not null).ParallelForEachAsync(async updateable => await updateable.UpdateParallelAsync(timeScale, data), 4);
        }
        
        public void UpdateOneThread(float timeScale)
        {
            foreach (var updateable in this)
                updateable.UpdateOneThread(timeScale);
        }

        public async Task UpdateParallelAsync(float timeScale)
        {
            await this.ParallelForEachAsync(async updateable => await updateable.UpdateParallelAsync(timeScale), 4);
        }
    }
}