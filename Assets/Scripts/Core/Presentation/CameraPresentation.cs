using Core.Entities;
using UnityEngine;
using Wooff.ECS;
using Wooff.Presentation;

namespace Core.Presentation
{
    public class CameraPresentation : MonoEntity<MainCamera>
    {
        public override IInitable Init()
        {
            return this;
        }
    }
}