using System.Collections.Generic;
using Core;
using Core.Components.Tags;
using Core.Components.Tags.UiTags;
using Core.Components.Tags.UiTags.Windows;
using Core.Components.UnityRelated;
using Core.Systems;
using Core.Systems.ClickSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wooff.ECS.Contexts;
using Wooff.ECS.Worlds;

namespace Wooff.MonoIntegration
{
    public class MonoWorld : MonoBehaviour, IWorld
    {
        public EntityContext EntityContext { get; } = new EntityContext();
        public SystemContext SystemContext { get; } = new SystemContext();

        [Header("Camera")] 
        [SerializeField] private CameraHandlerTagComponentData _cameraHandlerComponentsGroup;
        
        [Header("Cell Generator")]
        [SerializeField] private CellGenerator _cellGenerator;

        [Header("Window Manager")] 
        [SerializeField] private UnityGameObjectComponent _windowContextObjectComponent;

        [Header("Players Configs")] 
        [SerializeField]
        private List<PlayerTagComponentData> _playersComponents;
        
        private void Awake()
        {
            SystemContext.ContextAdd(new UnityObjectInitialization());
            SystemContext.ContextAdd(new UiRelatedComponentsInitialization());
            SystemContext.ContextAdd(new PlayerCameraControl());
            SystemContext.ContextAdd(new SmoothRotateAround());
            SystemContext.ContextAdd(new UpdateSmoothTranslate());
            SystemContext.ContextAdd(new PlayerTagVisualisation());
            SystemContext.ContextAdd(new MetricUserBalanceShower());
            
            SystemContext.ContextAdd(new CameraMouseClick());
            SystemContext.ContextAdd(new MoveCameraOnCellClick());
            SystemContext.ContextAdd(new OpenInformationWindowOnCellClick());
            SystemContext.ContextAdd(new OpenChooseCellWindowOnCellClick());
            SystemContext.ContextAdd(new EndProcessClickStateCell());
            
            SystemContext.ContextAdd(new HealthTracker());
            SystemContext.ContextAdd(new MetricBalanceMining());
            SystemContext.ContextAdd(new PlayersTurns());
            SystemContext.ContextAdd(new WindowTracker());
        }

        private void Start()
        {
            // Spawn Cell entity container
            foreach (var cell in _cellGenerator.InitWorld())
                EntityContext.ContextAdd(cell);
            
            // Spawn Camera Entity container
            EntityContext
                .ContextAdd(new CameraHandlerTagComponent(_cameraHandlerComponentsGroup)
                    .CreateCameraEntityContainer());
            
            // Spawn Window Context Entity container
            EntityContext
                .ContextAdd(new WindowContextTagComponent(_windowContextObjectComponent)
                    .CreateWindowContextEntityContainer());
            
            EntityContext
                .ContextAdd(new MetricShowerWindowTagComponent(UiComponentsDataPrefabsHandler.MetricShowerTagComponentData)
                        .CreateWindowEntityContainer());

            foreach (var component in _playersComponents)
                EntityContext.ContextAdd(new PlayerTagComponent(component).CreatePlayerTagEntityContainer());
            
            SystemContext.Start(EntityContext);
        }

        private void Update()
        {
            SystemContext.Update(1f, EntityContext);

            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene("BenchmarkECS");
        }
        
        /// <summary>
        /// Spawn Prefab in world and set his name to prefab name
        /// </summary>
        /// <param name="prefab">prefab of object</param>
        /// <returns>spawned object</returns>
        public static GameObject SpawnEntity(GameObject prefab)
        {
            var gameObject = Instantiate(prefab);
            gameObject.name = prefab.name; 
            return gameObject;
        }
    }
}
