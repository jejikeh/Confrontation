using System.Collections.Generic;
using System.Linq;
using Core.Components.PlayerComponent;
using Core.Components.Properties.PropertyComponent;
using Core.Components.Properties.PropertyOwnerComponent;
using Core.Entities.PropertyTagIconVisualisation;
using DG.Tweening;
using UnityEngine;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class PlayerOwnerIconsVisualization : IMonoSystem
    {
        private List<Property> _properties = new List<Property>();

        public void Process(float timeScale, IContext<IMonoEntity, List<IMonoEntity>> data)
        {
            var players = data.Items
                .Select(x => x.ContextGetAs<Player>())
                .Where(x => x is not null)
                .Where(x => x.Config.PlayerType != PlayerType.None);

            foreach (var player in players.ToArray())
            {
                foreach (var property in player.Handler.Items
                             .ToArray()
                             .Select(x => x.Handler.ContextGet<PropertyHandler>())
                             .SelectMany(x => x.Items))
                {
                    if(_properties.Contains(property))
                        continue;
                    
                    var propertyTagPresentation = 
                        MonoWorld.SpawnEntity<PropertyTagPresentation>(
                            player.Config.VisualizeIcon,
                            ((Property)property).Handler.MonoObject.transform.position + Vector3.down);

                    propertyTagPresentation.MonoObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = player.Config.VisualizeColor;
                    propertyTagPresentation.MonoObject.transform.SetParent((property as Property)?.Handler.MonoObject.transform);
                    propertyTagPresentation.MonoObject.transform.DOLocalMove(Vector3.up, 0.5f);
                    _properties.Add(property as Property);
                }
            }
        }
    }
}