using System.Threading.Tasks;
using UnityEngine;

namespace Kore
{
    public class Kystem : MonoBehaviour
    {
        public virtual void Update()
        {
        }

        public bool Enabled { get; private set; }
        
        public async void Enable()
        {
            if (Enabled)
                return;

            await OnEnable();
            Enabled = true;
        }
        
        public async void Disable()
        {
            if (!Enabled)
                return;
            
            await OnDisable();
            Enabled = false;
        }
        
        protected virtual Task OnEnable()
        {
            return Task.CompletedTask;
        }
        
        protected virtual Task OnDisable()
        {
            return Task.CompletedTask;
        }
    }
}