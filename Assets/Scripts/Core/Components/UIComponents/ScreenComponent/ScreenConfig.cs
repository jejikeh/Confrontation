using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components.UIComponents.WindowComponent;
using Core.Entities.UI;
using UnityEngine;
using Wooff.ECS;
using Wooff.MonoIntegration;

namespace Core.Components.UIComponents.ScreenComponent
{
    [Serializable]
    public class ScreenConfig : IConfig
    {
        public List<MonoWindow> WindowConfigs = new List<MonoWindow>();
        public ScreenState ScreenState;
        
        public MonoWindow GetWindow(WindowType windowType)
        {
            return WindowConfigs.FirstOrDefault(x => x.WindowType == windowType);
        }
    }
}