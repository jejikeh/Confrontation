using System;
using UnityEngine.Events;

namespace Entities.UI.WindowSystem.Windows
{
    [Serializable]
    public class WindowEntry
    {
        public string EntryName;
        public UnityEvent Callback;
    }
}