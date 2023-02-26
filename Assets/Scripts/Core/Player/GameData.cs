using System.Collections.Generic;
using UnityEngine;

namespace DungeonSurvivor.Core.Player
{
    [CreateAssetMenu]
    public class GameData : ScriptableObject
    {
        [Range(0, 100)] public int currentLevel;
        public List<Vector2Int> levelSizes;
        public GameObject prefabStand, prefabWater, prefabWall;
    }
}