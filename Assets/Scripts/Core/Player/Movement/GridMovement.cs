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
            Block block = GridManager.Instance.GetBlock(currentIndex.x, currentIndex.y, direction);
            if (block.type is not BlockType.None)
            {
                Move(block);
            }
        }

        private void Move(Block block)
        {
            Vector3 targetBlockPosition = block.transform.position;
            targetBlockPosition.y = 1f;
            currentIndex          = block.index;
            LeanTween.move(gameObject, targetBlockPosition, 0.1f)
                .setEaseInSine();
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