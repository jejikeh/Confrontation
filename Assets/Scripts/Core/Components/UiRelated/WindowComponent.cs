using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Core.Components.UiRelated
{
    public abstract class WindowComponent : IWindowComponent
    {
        public async Task OnOpen(Transform transform)
        {
            await transform.DOScale(Vector3.one, 1f).AsyncWaitForCompletion();
        }

        public async Task OnClose(Transform transform)
        {
            await transform.DOScale(Vector3.zero, 1f).AsyncWaitForCompletion();
        }
    }
}