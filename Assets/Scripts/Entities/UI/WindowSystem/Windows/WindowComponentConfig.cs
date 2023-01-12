using System.Collections.Generic;
using Core.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Entities.UI.WindowSystem.Windows
{
    public class WindowComponentConfig : ScriptableObject, ICustomComponentConfig
    {
        public WindowType Type;
        public VisualTreeAsset WindowAsset;
        public PanelSettings PanelSettings;
        // public List<WindowEntry> WindowEntries;
        public string WindowContainerName;
        public string ScreenContainerName;
    }
}