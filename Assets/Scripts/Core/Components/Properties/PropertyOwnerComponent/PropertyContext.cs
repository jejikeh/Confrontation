using System.Collections.Generic;
using Core.Components.Properties.PropertyComponent;
using Wooff.ECS.Contexts;

namespace Core.Components.Properties.PropertyOwnerComponent
{
    public class PropertyContext :
        Context<IProperty, List<IProperty>>, 
        IListContext<IProperty>
    {
        
    }
}