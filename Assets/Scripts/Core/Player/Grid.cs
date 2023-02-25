using System.Collections.Generic;
using UnityEngine;

namespace DungeonSurvivor.Core.Player
{
    public class Grid
    {
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        
        private readonly BlockType[,] blocks;
        private readonly Dictionary<Direction, Vector2Int> map = new()
        {
            { Direction.Up, Vector2Int.up },
            { Direction.Down, Vector2Int.down },
            { Direction.Left, Vector2Int.left },
            { Direction.Right, Vector2Int.right }
        };
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Cols = columns;
            
            blocks = new BlockType[Rows, Cols];
            
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Cols; j++)
                {
                    blocks[i, j] = BlockType.None;
                }
            }
        }
        public void SetBlock(Vector2Int index, BlockType blockType)
        {
            if (0 > index.x || index.x >= Rows || 0 > index.y || index.y >= Cols) return;
            blocks[index.x, index.y] = blockType;
        }
        public bool CanMove(Vector2Int index, Direction direction)
        {
            var nextIndex = index + map[direction];
            return blocks[nextIndex.x, nextIndex.y] == BlockType.Standing;
        }
    }
    public enum BlockType
    {
        None, Standing, Water, Wall
    }
    public enum Direction
    {
        Up, Down, Left, Right
    }
}