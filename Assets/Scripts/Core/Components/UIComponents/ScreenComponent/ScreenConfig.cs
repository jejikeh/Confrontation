using System;
using System.Collections.Generic;
using Core.Components.WindowComponent;

namespace Core.Components.WindowsHandlerComponent
{
    [Serializable]
    public class WindowsHandlerConfig
    {
        public List<WindowConfig> WindowConfigs = new List<WindowConfig>();
    }
}