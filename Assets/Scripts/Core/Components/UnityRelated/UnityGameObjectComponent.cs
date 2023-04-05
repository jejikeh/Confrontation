using System;
using UnityEngine;
using Wooff.ECS.Components;

namespace Core.Components.UnityRelated
{
    [Serializable]
    public class UnityGameObjectComponent : IComponent
    {
        public GameObject UnityPrefab;
        [HideInInspector] public GameObject UnitySceneObject;
        
        public Vector3 StartPosition;
        public Quaternion StartRotation;

        public UnityGameObjectComponent(UnityGameObjectComponent unityGameObjectComponent)
        {
            UnityPrefab = unityGameObjectComponent.UnityPrefab;
            UnitySceneObject = unityGameObjectComponent.UnitySceneObject;
            StartPosition = unityGameObjectComponent.StartPosition;
            StartRotation = unityGameObjectComponent.StartRotation;
        }
        
        public void InitPositionAndRotation()
        {
            UnitySceneObject.transform.position = StartPosition;
            UnitySceneObject.transform.rotation = StartRotation;
        }
    }
}
