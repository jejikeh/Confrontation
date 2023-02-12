using Core.Entities;
using UnityEngine;
using Wooff.ECS;
using Wooff.Presentation;

namespace Core.Presentation
{
    public class BobPresentation : MonoEntity<Bob>
    {
        public override IInitable Init()
        {
            return this;
        }
    }
}