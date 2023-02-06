namespace Wooff.ECS.Component
{

    public interface IComponent : IInitable
    {
    }

    public interface IComponent<T> : IComponent, IInitable<T>
    {

    }
}