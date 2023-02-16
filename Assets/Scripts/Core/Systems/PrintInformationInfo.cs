using System.Collections.Generic;
using System.Linq;
using Core.Components.InformationComponent;
using Wooff.ECS.Contexts;
using Wooff.Presentation;

namespace Core.Systems
{
    public class PrintInformationInfo : IMonoSystem
    {
        public void Process(float timeScale, IContext<IMonoEntity, List<IMonoEntity>> data)
        {
            foreach (var item in data.Items
                         .Where(x => x.ContextContains<Information>())
                         .Select(x => x.ContextGet<Information>()))
            {
                item?.WhoAmIm();
            }
        }
    }
}