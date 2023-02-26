using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DungeonSurvivor.Core.Player;
using DungeonSurvivor.Core.Managers;
using static DungeonSurvivor.Core.Events.Internal;

namespace DungeonSurvivor.Core.GridFunctionality
{
    public class GridManager : Singleton<GridManager>
    {
        [SerializeField] private List<Block> currentLevelBlocks;

        private void Awake()
        {
            currentLevelBlocks = GetComponentsInChildren<Block>().ToList();
            GridDataCollectionCompleted?.Invoke();
        }
        
        public Block GetBlock(Vector2Int index)
        {
            return GameManager.Instance.IsValidGridIndex(index)
                ? currentLevelBlocks.FirstOrDefault(blk => blk.index.x == index.x && blk.index.y == index.y)
                : null;
        }
    }
}