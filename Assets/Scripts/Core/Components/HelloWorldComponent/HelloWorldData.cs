using System;
using Wooff.Presentation;

namespace Core.Components.HelloWorldComponent
{
    [Serializable]
    public class HelloWorldData
    {
        public string Message;
    }
    
    [Serializable]
    public sealed class HelloWorldDictionary : DataDictionary<HelloWorldData>
    {
    }
}