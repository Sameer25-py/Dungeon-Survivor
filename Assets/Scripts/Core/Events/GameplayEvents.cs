using DungeonSurvivor.Core.Pickables;
using DungeonSurvivor.Core.Puzzles;
using UnityEngine;
using UnityEngine.Events;

namespace DungeonSurvivor.Core.Events
{
    public static class GameplayEvents
    {
        public static class Movement
        {
            public static readonly UnityEvent<Vector3>         MoveToPosition  = new();
            public static readonly UnityEvent<int, Vector2Int> MoveInDirection = new();
        }

        public static class Puzzles
        {
            public static readonly UnityEvent<Teleport>
                TeleportPosition = new();

            public static readonly UnityEvent<Color> GateOpen  = new();
            public static readonly UnityEvent<Color> GateClose = new();
        }

        public static class MiniGames
        {
            public static readonly UnityEvent MiniGameCompleted = new();

            public static class PatternMatch
            {
                public static readonly UnityEvent<int> MatchItemClicked = new();
            }
        }

        public static class Timer
        {
            public static readonly UnityEvent<float> CountDownTimePassed = new();
            public static readonly UnityEvent        CountDownEnded      = new();
        }

        public static class Inventory
        {
            public static readonly UnityEvent<int, PickableData> AddItem               = new();
            public static readonly UnityEvent<int>               ItemAddedSuccessfully = new();
            public static readonly UnityEvent                    AddItemFailed         = new();
        }

        public static class Camera
        {
            public static readonly UnityEvent SwitchToMiniGameCamera     = new();
            public static readonly UnityEvent SwitchToPlayerFollowCamera = new();
            public static readonly UnityEvent SwitchToEndLevelCamera     = new();
        }

        public static class Light
        {
            public static readonly UnityEvent DungeonLightsLitUp = new();
        }
    }
}