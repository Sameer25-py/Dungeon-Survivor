using DungeonSurvivor.Core.GridFunctionality;
using DungeonSurvivor.Core.Pickables;
using DungeonSurvivor.Core.Pushables;
using UnityEngine;
using UnityEngine.Events;

namespace DungeonSurvivor.Core.Events
{
    public static class Internal
    {
        public static readonly UnityEvent                        GridDataCollectionCompleted = new();
        public static readonly UnityEvent<PushableBase>          PushableDestroyed           = new();
        public static readonly UnityEvent<Vector2Int, BlockType> ChangeBlockType             = new();
        public static readonly UnityEvent<PickableBase>          PickableDestroyed           = new();
        public static readonly UnityEvent<Sprite>                UpdateInventoryUI           = new();
        public static readonly UnityEvent                        EnableLevelEnd              = new();
        public static readonly UnityEvent                        ShowSceneLoader             = new();
        public static readonly UnityEvent                        HideSceneLoader             = new();
        public static readonly UnityEvent<float>                 SceneLoaderProgress         = new();
    }
}