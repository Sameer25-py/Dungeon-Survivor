using UnityEngine;
using DungeonSurvivor.Core.GridFunctionality;
using static DungeonSurvivor.Core.Events.GameplayEvents.Movement;


namespace DungeonSurvivor.Core.Player.Movement
{
    public class GridMovement : MonoBehaviour
    {
        [SerializeField] private Vector2Int currentIndex;
        private Vector3 targetPosition;

        private void OnMoveInDirectionCalled(Vector2Int direction)
        {
            var block = GridManager.Instance.GetBlock(currentIndex + direction);

            if (!block) return;
            if (block.type is BlockType.None) return;
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f) return;
            Move(block);
        }

        private void Move(Block block)
        {
            targetPosition = block.transform.position;
            targetPosition.y = 1f;
            currentIndex          = block.index;
            LeanTween.move(gameObject, targetPosition, 0.1f)
                .setEaseInSine();
        }

        private void OnEnable()
        {
            MoveInDirection.AddListener(OnMoveInDirectionCalled);
            targetPosition = transform.position;
        }

        private void OnDisable()
        {
            MoveInDirection.RemoveListener(OnMoveInDirectionCalled);
        }
    }
}