using System.Threading.Tasks;
using UnityEngine;
using Wooff.Presentation;
using Mesh = Core.Components.MeshComponent.Mesh;

namespace Core.Systems
{
    public class MeshSystem : MonoSystem
    {
        protected override void SystemStartOneThread(IMonoEntity item)
        {
            if(!item.Contains<Mesh>())
                return;
                
            item.GetFirst<Mesh>()?.SetMesh(item);
        }
    }
}