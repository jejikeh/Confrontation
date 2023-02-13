using UnityEngine;
using Wooff.ECS.Context;

namespace Wooff.Presentation
{
    public class MonoEntityContext : UpdateableContext<IMonoEntity>
    {
        public new T1 Add<T1>() where T1 : IMonoInitable, IMonoEntity
        {
            var monoEntityGameObject = new GameObject(
                typeof(T1).FullName,
                typeof(T1));
            var entity = monoEntityGameObject.GetComponent<T1>();
            entity.MonoObject = monoEntityGameObject;
            (entity as IMonoInitable).Init();
            Add(entity);
            return entity;
        }
        
        public new T1 Add<T1, T2>(T2 data) where T1 : IMonoInitable<T2>, IMonoEntity
        {
            var monoEntityGameObject = new GameObject(
                typeof(T1).FullName,
                typeof(T1));
            var entity = monoEntityGameObject.GetComponent<T1>();
            entity.MonoObject = monoEntityGameObject;
            entity.Init(data);
            Add(entity);
            return entity;
        }
    }
}