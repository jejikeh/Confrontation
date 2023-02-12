using System.Threading.Tasks;
using Core.Components.HelloWorldComponent;
using UnityEngine;
using Wooff.Presentation;
using Mesh = Core.Components.MeshComponent.Mesh;

namespace Core.Systems
{
    public class HelloWorldUpdatePrint : MonoSystem
    {
        protected override void SystemStartOneThread(IMonoEntity item)
        {
            if(!item.Contains<HelloWorld>())
                return;
            
            item.GetFirstNullable<HelloWorld>()?.Speak();
            item.MonoObject.transform.position = new Vector3(
                Random.Range(-10, 10),
                Random.Range(-10, 10),
                Random.Range(-10, 10));
        }

        protected override Task SystemUpdateParallelAsync(float timeScale, IMonoEntity updateItem)
        {
            if(!updateItem.Contains<HelloWorld>())
                return Task.WhenAll();

            updateItem.GetFirstNullable<HelloWorld>()?.Speak();
            return Task.WhenAll();
        }

        protected override void SystemUpdateOneThread(float timeScale, IMonoEntity updateItem)
        {
            if(!updateItem.Contains<HelloWorld>())
                return;
            
            updateItem.MonoObject.gameObject.transform.Translate(new Vector3(
                Random.Range(-2, 2),
                Random.Range(-2, 2),
                Random.Range(-2, 2)) * Time.deltaTime);
        }
    }
}