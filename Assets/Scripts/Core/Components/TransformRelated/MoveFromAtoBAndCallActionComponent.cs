using System;
using Wooff.ECS.Components;
using Wooff.ECS.Entities;

namespace Core.Components.TransformRelated
{
    public class MoveFromAtoBComponent : IComponent
    {
        public IEntity APoint;
        public IEntity BPoint;
        public Action<IEntity, IEntity> ActionAfterMovement;

        public MoveFromAtoBComponent(IEntity fromA, IEntity toB, Action<IEntity, IEntity> actionAfterMovement)
        {
            APoint = fromA;
            BPoint = toB;
            ActionAfterMovement = actionAfterMovement;
        }
    }
}