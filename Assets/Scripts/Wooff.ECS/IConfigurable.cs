namespace Wooff.ECS
{
    public interface IConfigurable<out T> where T : IConfig
    {
        public T Config { get; }
    }
}