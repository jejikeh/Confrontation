namespace Wooff.ECS
{
    public interface IConfigurable<T> where T : IConfig
    {
        public T Config { get; }
    }
}