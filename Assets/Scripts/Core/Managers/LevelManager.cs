using UnityEngine;
using DungeonSurvivor.Core.GridFunctionality;

namespace DungeonSurvivor.Core.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private Vector2Int levelSize;
        private GridFunctionality.Grid grid;

        protected override void BootOrderAwake()
        {
            grid = new GridFunctionality.Grid(levelSize.x, levelSize.y);
            var blocks = FindObjectsByType<Block>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var block in blocks)
            {
                grid.SetBlock(block.index, block.type);
            }

            base.BootOrderAwake();
        }

        public bool IsValidGridIndex(Vector2Int index)
        {
            return grid.CanMove(index);
        }

        public Vector2Int GetGridSize() => levelSize;
    }
}