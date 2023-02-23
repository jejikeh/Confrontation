using System.Collections.Generic;
using Core.Components.Properties.PropertyComponent;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Components.Properties.PropertyOwnerComponent
{
    public class PropertyHandler :
        Component<IConfig, IMonoEntity>, 
        IListContext<IProperty>
    {
        public List<IProperty> Items => _propertyContext.Items;
        private readonly PropertyContext _propertyContext;

        public PropertyHandler(IConfig data, IMonoEntity handler) : base(data, handler)
        {
            _propertyContext = new PropertyContext();
        }
        
        public IProperty ContextAdd(IProperty item)
        {
            item.ChangePropertyHandler(this);
            return _propertyContext.ContextAdd(item);
        }

        public T2 ContextGet<T2>() where T2 : class, IProperty
        {
            return _propertyContext.ContextGet<T2>();
        }

        public bool ContextRemove(IProperty item)
        {
            return _propertyContext.ContextRemove(item);
        }

        public bool ContextContains<T2>() where T2 : class, IProperty
        {
            return _propertyContext.ContextContains<T2>();
        }
    }
}