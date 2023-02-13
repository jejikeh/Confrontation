namespace Wooff.ECS
{
    public interface IStartable
    {
        public void StartOneThread();
    }
    
    public interface IStartable<in T> : IStartable
    {
        public void StartOneThread(T data);
    }
}