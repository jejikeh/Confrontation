using UnityEngine;

namespace Core.Components.HelloWorldComponent
{
    public class HelloWorld : CoreComponent<HelloWorldData>
    {
        public void Speak()
        {
            Debug.Log(Data.Message);
        }
    }
}