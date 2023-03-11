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
        // TODO: cache not the entities count but count from map component|list entity 
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
                var smoothRotate = entity.ContextGet<SmoothRotateComponent>();

                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    smoothRotate.NewRotation,
                    Time.deltaTime * timeScale * smoothRotate.RotationTime);
            }
        }
    }
}