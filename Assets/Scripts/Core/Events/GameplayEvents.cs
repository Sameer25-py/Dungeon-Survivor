using UnityEngine;
using UnityEngine.Events;
using DungeonSurvivor.Core.Puzzles;

namespace DungeonSurvivor.Core.Events
{
    public static class GameplayEvents
    {
        public static class Movement
        {
            public static readonly UnityEvent<Vector3> MoveToPosition = new();
        }
        
        public static class Puzzles
        {
            public static readonly UnityEvent<Teleport> TeleportPosition = new();
        }
    }
}