using System.Linq;
using DungeonSurvivor.Core.Data;
using DungeonSurvivor.Core.GridFunctionality;
using UnityEditor;
using UnityEngine;

namespace DungeonSurvivor.Core.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        public  GameData                 data;
        private GridFunctionality.Grid[] levelGrids;

        protected override void BootOrderAwake()
        {
            levelGrids = new GridFunctionality.Grid[data.levelSizes.Count];
            for (var i = 0; i < data.levelSizes.Count; i++)
            {
                levelGrids[i] = new GridFunctionality.Grid(data.levelSizes[i].x, data.levelSizes[i].y);
            }

            var blocks = FindObjectsByType<Block>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var block in blocks)
            {
                levelGrids[block.level].SetBlock(block.index, block.type);
            }

            base.BootOrderAwake();
        }

        public bool IsValidGridIndex(Vector2Int index)
        {
            return levelGrids[0].CanMove(index);
        }

        public Vector2Int GetGridSize()
        {
            return new Vector2Int(levelGrids[0]
                .rows, levelGrids[0]
                .cols);
        }
        
        #if UNITY_EDITOR
        [MenuItem("Dungeon Survivor/Show Block Indexes")]
        public static void ShowIndexes()
        {
            FindObjectsByType<Block>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .ToList().ForEach(blk => blk.OnDrawGizmos());
        }
        #endif
    }
}