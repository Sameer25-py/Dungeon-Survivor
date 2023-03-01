using DungeonSurvivor.Core.Pushables;
using UnityEngine.Events;

namespace DungeonSurvivor.Core.Events
{
    public static class Internal
    {
        public static readonly UnityEvent               GridDataCollectionCompleted = new();
        public static readonly UnityEvent<PushableBase> PushableDestroyed           = new();
    }
}