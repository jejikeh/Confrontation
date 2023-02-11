using System.Threading.Tasks;
using Core.Components.HelloWorldComponent;
using UnityEngine;
using Wooff.Presentation;

namespace Core.Systems
{
    public class HelloWorldUpdatePrint : MonoSystem
    {
        protected override Task SystemUpdateAsync(float timeScale, IMonoEntity updateItem)
        {
            if(!updateItem.Contains<HelloWorld>())
                return Task.WhenAll();

            updateItem.GetFirstNullable<HelloWorld>()?.Speak();
            return Task.WhenAll();
        }

        protected override void SystemUpdate(float timeScale, IMonoEntity updateItem)
        {
            if(!updateItem.Contains<HelloWorld>())
                return;
            
            updateItem.MonoObject.gameObject.transform.Translate(new Vector3(0,1,0));
        }
    }
}