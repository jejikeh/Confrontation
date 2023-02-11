using System.Threading.Tasks;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Component;
using Wooff.Presentation;

namespace Core.Components.HelloWorldComponent
{
    public class HelloWorld : CoreComponent<HelloWorldData>
    {
        public void Speak()
        {
            Debug.Log("Hello World");
        }
    }
}