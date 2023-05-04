using System;
using Wooff.ECS.Components;
using Wooff.ECS.Entities;

namespace Core.Components.TransformRelated
{
    public class MoveFromAtoBAndCallActionComponent : IComponent
    {
        public IEntity APoint;
        public IEntity BPoint;
        public Action<IEntity, IEntity, int> ActionAfterMovement;

        public MoveFromAtoBAndCallActionComponent(IEntity fromA, IEntity toB, Action<IEntity, IEntity, int> actionAfterMovement)
        {
            APoint = fromA;
            BPoint = toB;
            ActionAfterMovement = actionAfterMovement;
        }
    }
}