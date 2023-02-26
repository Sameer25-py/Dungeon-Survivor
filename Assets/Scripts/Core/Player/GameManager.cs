using DungeonSurvivor.Core.Data;
using DungeonSurvivor.Core.Grid;
using UnityEngine;
using DungeonSurvivor.Core.Managers;

namespace DungeonSurvivor.Core.Player
{
    public class GameManager : Singleton<GameManager>
    {
        public  GameData         data;
        private Core.Grid.Grid[] levelGrids;
        protected override void BootOrderAwake()
        {
            levelGrids = new Core.Grid.Grid[data.levelSizes.Count];
            for (var i = 0; i < data.levelSizes.Count; i++)
            {
                levelGrids[i] = new Core.Grid.Grid(data.levelSizes[i]
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
