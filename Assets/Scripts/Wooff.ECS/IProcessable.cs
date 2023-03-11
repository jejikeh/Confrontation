namespace Wooff.ECS
{
    public interface IProcessable 
    {
        public void Start();
        public void Update(float timeScale);
    }

    public interface IProcessable<in T> 
    {
        public void Start(T data);
        public void Update(float timeScale, T data);
    }
}