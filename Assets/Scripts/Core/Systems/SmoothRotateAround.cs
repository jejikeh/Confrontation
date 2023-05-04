using System.Linq;
using Core.Components.TransformRelated;
using Core.Components.UnityRelated;
using UnityEngine;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems
{
    public class SmoothRotateAround : Wooff.ECS.Systems.System
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
                
            foreach (var entity in _cachedEntities)
            {
                var transform = entity.ContextGet<UnityGameObjectComponent>().UnitySceneObject.transform;
                if (!entity.ContextContains<SmoothRotateComponent>())
                    return;
                
                var smoothRotate = entity.ContextGet<SmoothRotateComponent>();
                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    smoothRotate.NewRotation,
                    Time.deltaTime * timeScale * smoothRotate.RotationTime);
            }
        }
    }
}