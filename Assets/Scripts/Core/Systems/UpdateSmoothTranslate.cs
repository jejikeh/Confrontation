using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Components.TransformRelated;
using Core.Components.UnityRelated;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class UpdateSmoothTranslate : Wooff.ECS.Systems.System
    {
        private IEntity[] _cachedEntities;
        private int _cachedCount;

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            
            if (_cachedCount != context.Count<SmoothTranslateComponent>())
            {
                _cachedEntities = context.ContextWhereQuery(x =>
                    x.ContextContains<UnityGameObjectComponent>() &&
                    x.ContextContains<SmoothTranslateComponent>()).ToArray();

                _cachedCount = context.Count<SmoothTranslateComponent>();
            }

            foreach ( var entity in _cachedEntities) 
            {
                var transformWrapper = entity.ContextGet<UnityGameObjectComponent>();
                var smoothTranslate = entity.ContextGet<SmoothTranslateComponent>();

                smoothTranslate.UpdatePosition(timeScale, transformWrapper.UnitySceneObject.transform);
                smoothTranslate.UpdateVelocity(timeScale, transformWrapper.UnitySceneObject.transform);
            }
        }
    }
}