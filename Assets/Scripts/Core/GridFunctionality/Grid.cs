using UnityEngine;

namespace DungeonSurvivor.Core.GridFunctionality
{
    public class Grid
    {
        public readonly int          rows;
        public readonly int          cols;
        public readonly BlockType[,] blocks;

        public Grid(int rows, int columns)
        {
            this.rows = rows;
            this.cols = columns;

            blocks = new BlockType[this.rows, cols];

            for (var i = 0; i < this.rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    blocks[i, j] = BlockType.None;
                }
            }
        }

        public void SetBlock(Vector2Int index, BlockType blockType)
        {
            if (0 > index.x || index.x >= rows || 0 > index.y || index.y >= cols) return;
            blocks[index.x, index.y] = blockType;
        }

        public bool CanMove(Vector2Int index)
        {
            return (index.x >= 0 && index.x < rows) && (index.y >= 0 && index.y < cols) &&
                   blocks[index.x, index.y] == BlockType.Standing;
        }
    }

    public enum BlockType
    {
        None,
        Standing,
        Water,
        Wall
    }
}