using UnityEngine;

namespace Kore.Polygon.HelloPrinter
{
    public class Message : Komponent<MessageData>
    {
        public Message(MessageData data) : base(data)
        {
            
        }
        
        public void SayHello() => Debug.Log($"Hello World! {Data.Message}");
    }
}