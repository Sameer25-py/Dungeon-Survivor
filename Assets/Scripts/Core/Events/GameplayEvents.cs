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

            public static readonly UnityEvent<int> GateOpen  = new();
            public static readonly UnityEvent<int> GateClose = new();
        }

        public static class MiniGames
        {
            public static class PatternMatch
            {
                public static readonly UnityEvent<int> MatchItemClicked = new();
            }
        }

        public static class Timer
        {
            public static readonly UnityEvent<float> CountDownTimePassed = new();
        }

        public static class Inventory
        {
            public static readonly UnityEvent<int, PickableData> AddItem               = new();
            public static readonly UnityEvent<int>               ItemAddedSuccessfully = new();
            public static readonly UnityEvent                    AddItemFailed         = new();
        }
    }
}