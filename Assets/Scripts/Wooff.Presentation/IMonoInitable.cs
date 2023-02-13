namespace Wooff.Presentation
{
    public interface IMonoInitable
    {
        public IMonoInitable Init();
    }

    public interface IMonoInitable<T> : IMonoInitable
    {
        public IMonoInitable<T> Init(T data);
    }
}