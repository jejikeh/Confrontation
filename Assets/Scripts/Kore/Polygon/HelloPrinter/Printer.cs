namespace Kore.Polygon.HelloPrinter
{
    public class Printer : Kentity
    {
        private void Start()
        {
            Message.AttachTo(
                this, 
                new MessageData() { Message = "AHAHA"},
                message => new Message(message)); 
        }
    }
}