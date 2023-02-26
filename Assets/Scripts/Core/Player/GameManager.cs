using DungeonSurvivor.Core.Data;
using DungeonSurvivor.Core.GridFunctionality;
using UnityEngine;
using DungeonSurvivor.Core.Managers;

namespace DungeonSurvivor.Core.Player
{
    public class GameManager : Singleton<GameManager>
    {
        public  GameData                 data;
        private GridFunctionality.Grid[] levelGrids;
        protected override void BootOrderAwake()
        {
            levelGrids = new GridFunctionality.Grid[data.levelSizes.Count];
            for (var i = 0; i < data.levelSizes.Count; i++)
            {
                levelGrids[i] = new GridFunctionality.Grid(data.levelSizes[i]
                    .x, data.levelSizes[i]
                    .y);
            }

            var blocks = FindObjectsByType<Block>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var block in blocks)
            {
                levelGrids[block.level]
                    .SetBlock(block.index, block.type);
            }

            base.BootOrderAwake();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Check(new Vector2Int(i, j));
                }
            }
        }
        private void Check(Vector2Int index)
        {
            print($"Can move on {index}? => {levelGrids[0].CanMove(index)}");
        }

        public bool IsValidGridIndex(Vector2Int index)
        {
            return levelGrids[0]
                .IsValidBlock(index);
        }
        
    }
}
