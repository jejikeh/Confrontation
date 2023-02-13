using System;
using Core.Entities;
using Wooff.ECS;
using Wooff.Presentation;

namespace Core.Presentation
{
    public class BobPresentation : MonoEntity<Bob>, IMonoInitable
    {
        public override IInitable Init()
        {
            return this;
        }

        IMonoInitable IMonoInitable.Init()
        {
            return this;
        }
    }
}