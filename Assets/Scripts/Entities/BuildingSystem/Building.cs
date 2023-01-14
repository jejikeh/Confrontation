using System;
using System.Threading.Tasks;
using CustomComponents.MouseClickable;
using DG.Tweening;
using Entities.BuildingSystem.Buildings;
using Entities.UI.WindowSystem;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Managers.PlayerInput;

namespace Entities.BuildingSystem
{
    [RequireComponent(typeof(MeshCollider))]
    public class Building : Entity<IBuildingComponent>
    {
        private MouseClickableComponent _mouseClickableComponent;
        
        // TODO: if the building does not manage its constant state by itself, then it will need to be removed
        public BuildSystemManagerConfig BuildSystemManagerConfig;
        private MeshCollider _meshCollider;
        
        private void Start()
        {
            _mouseClickableComponent = (MouseClickableComponent)AddCustomComponent(
                new MouseClickableComponent(UnityEngine.Camera.main, transform));
            
            _meshCollider = GetComponent<MeshCollider>();
            PlayerInput.Input.DebugBuildingMine.performed += DebugBuildingsPerform;
            PlayerInput.Input.DebugBuildingFarm.performed += DebugBuildingFarmPerform;
            PlayerInput.Input.DebugBuildingGrass.performed += DebugBuildingGrassPerform;
            _mouseClickableComponent.OnMouseClicked += MouseClickableComponentOnOnMouseClicked;
        }

        private static async void MouseClickableComponentOnOnMouseClicked(object sender, EventArgs e)
        {
            await ScreenManager.OpenWindowAsync(WindowType.Input, "panel-container");

        }

        private async void DebugBuildingGrassPerform(InputAction.CallbackContext obj)
        {
            await SetConstantState(BuildSystemManagerConfig.CreateBuildingComponent(BuildingType.Grass));
        }

        private async void DebugBuildingFarmPerform(InputAction.CallbackContext obj)
        {
            await SetConstantState(BuildSystemManagerConfig.CreateBuildingComponent(BuildingType.Farm));
        }

        private async void DebugBuildingsPerform(InputAction.CallbackContext obj)
        {
            await SetConstantState(BuildSystemManagerConfig.CreateBuildingComponent(BuildingType.Mine));
        }

        private void Update()
        {
            UpdateCustomComponents();
        }
        
        protected override async Task OnConstantStateAssign()
        {
            await InstantiateBuildingMesh(ConstantStateComponent?.BuildingComponentConfig.Model);
        }

        protected override Task OnConstantStateRemove()
        {
            DestroyBuildingMesh();
            return Task.CompletedTask;
        }
        
        private async Task InstantiateBuildingMesh(GameObject mesh)
        {
            var objectTransform = Instantiate(mesh, transform).transform;
            objectTransform.localScale = Vector3.zero;
            _meshCollider.sharedMesh = gameObject.GetComponentInChildren<MeshFilter>().mesh;
            await objectTransform.DOScale(Vector3.one, 1f).AsyncWaitForCompletion();
        }
        
        private void DestroyBuildingMesh()
        {
            foreach (Transform mesh in transform)
                Destroy(mesh.gameObject);
        }
    }
}