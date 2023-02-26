using System;
using System.Collections.Generic;
using System.Linq;
using DungeonSurvivor.Core.Managers;
using DungeonSurvivor.Core.Player;
using UnityEngine;
using static DungeonSurvivor.Core.Events.Internal;

namespace DungeonSurvivor.Core.GridFunctionality
{
    public class GridManager : Singleton<GridManager>
    {
        [SerializeField] private List<Block> currentLevelBlocks;

        private void GetCurrentLevelBlocks()
        {
            currentLevelBlocks = GetComponentsInChildren<Block>()
                .ToList();
            GridDataCollectionCompleted?.Invoke();
        }

        private Block GetBlockByIndex(int row, int column)
        {
            Block blk = new();
            foreach (Block block in currentLevelBlocks.Where(block => block.index.x == row && block.index.x == column))
            {
                blk = block;
            }

            return blk;
        }

        public Block GetBlock(int row, int column, Direction direction)
        {
            Block      block = new();
            Vector2Int newIndex;
            switch (direction)
            {
                case Direction.Up:
                    newIndex = new Vector2Int(row, column - 1);
                    break;
                case Direction.Down:
                    newIndex = new Vector2Int(row, column + 1);
                    break;
                case Direction.Left:
                    newIndex = new Vector2Int(row - 1, column);
                    break;
                case Direction.Right:
                    newIndex = new Vector2Int(row + 1, column);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            if (GameManager.Instance.IsValidGridIndex(newIndex))
            {
                block = GetBlockByIndex(row + 1, column);
            }

            return block;
        }

        private void Start()
        {
            GetCurrentLevelBlocks();
        }
    }

    [Serializable]
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}