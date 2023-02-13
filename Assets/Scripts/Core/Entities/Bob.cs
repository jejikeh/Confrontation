using Core.Components.MeshComponent;
using Core.Components.TargetComponent;
using UnityEngine;
using Wooff.ECS.Entity;
using Wooff.Presentation;

namespace Core.Entities
{
    public class Bob : Entity<IMonoComponent>, IMonoEntity
    {
        public Bob()
        {
            Add<Components.MeshComponent.Mesh, MeshData>(DataStorage.GetMeshData("Bob"));
            Add<CameraSmoothLookAtTarget>();
        }

        protected override void OnUpdateableContextUpdate(float timeScale)
        {
            MonoObject.transform.Translate(new Vector3(
                    Random.Range(-0.1f,0.1f),
                    Random.Range(-0.1f,0.1f),
                    Random.Range(-0.1f,0.1f)) * Time.deltaTime * 100f);
        }

        public GameObject MonoObject { get; set; }
    }
}