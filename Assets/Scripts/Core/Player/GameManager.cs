using UnityEngine;
using System.Collections.Generic;
using DungeonSurvivor.Core.Managers;

namespace DungeonSurvivor.Core.Player
{
    public class GameManager : Singleton<GameManager>
    {
        public GameData Data;
        
        private Grid[] levelGrids;

        protected override void BootOrderAwake()
        {
            levelGrids = new Grid[Data.levelSizes.Count];
            for (var i = 0; i < Data.levelSizes.Count; i++)
            {
                levelGrids[i] = new Grid(Data.levelSizes[i].x, Data.levelSizes[i].y);
            }
            
            var blocks = FindObjectsByType<Block>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var block in blocks)
            {
                levelGrids[block.level].SetBlock(block.index, block.type);
            }
            
            base.BootOrderAwake();
        }
    }
    
    
}
