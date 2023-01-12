using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Entities.UI.WindowSystem;
using Entities.UI.WindowSystem.Windows;
using UnityEngine;
using UnityEngine.UIElements;

namespace Entities.UI.ScreenSystem.Screens
{
    public class ScreenComponentConfig : ScriptableObject, ICustomComponentConfig
    {
        public ScreenType Type;
        public VisualTreeAsset VisualTreeAsset;
        public PanelSettings PanelSettings;
        public List<WindowComponentConfig> HandledWindows;
        public string RootContainerName;

        public bool CanOpenWindow(WindowType windowType)
        {
            var config = HandledWindows.Find(x => x.Type == windowType);
            return config != null;
        }
        
        public T FindWindowConfig<T>() where T : WindowComponentConfig
        {
            var requiredConfig = HandledWindows.FirstOrDefault(x => typeof(T) == x.GetType());
            if (requiredConfig is null)
                throw new Exception("The config was not find in ScreenComponentConfig");

            return requiredConfig as T;
        }
    }
}