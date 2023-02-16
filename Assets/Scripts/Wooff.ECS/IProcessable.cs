namespace Wooff.ECS
{
    public interface IProcessable 
    {
        public void Process(float timeScale);
    }

    public interface IProcessable<T> 
    {
        public void Process(float timeScale, T data);
    }
}