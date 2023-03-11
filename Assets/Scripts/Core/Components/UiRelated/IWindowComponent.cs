using System.Threading.Tasks;
using UnityEngine;
using Wooff.ECS.Components;

namespace Core.Components.UiRelated
{
    public interface IWindowComponent : IComponent
    {
        public Task OnOpen(Transform transform);
        public Task OnClose(Transform transform);
    }
}