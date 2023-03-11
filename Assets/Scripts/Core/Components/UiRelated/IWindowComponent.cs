using System.Threading.Tasks;
using UnityEngine;

namespace Core.Components.UiRelated
{
    public interface IWindow
    {
        public Task OnOpen();
        public Task OnClose();
    }
}