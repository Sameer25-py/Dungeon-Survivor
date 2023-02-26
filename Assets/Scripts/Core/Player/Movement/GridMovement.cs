using DungeonSurvivor.Core.GridFunctionality;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Movement;


namespace DungeonSurvivor.Core.Player.Movement
{
    public class GridMovement : MonoBehaviour
    {
        [SerializeField] private Vector2Int currentIndex;
        private void OnMoveInDirectionCalled(Direction direction)
        {
            GridManager.Instance.GetBlock(currentIndex.x, currentIndex.y, direction);
            
        }
        private void OnEnable()
        {
            MoveInDirection.AddListener(OnMoveInDirectionCalled);
        }

        private void OnDisable()
        {
            MoveInDirection.RemoveListener(OnMoveInDirectionCalled);
        }
    }
}