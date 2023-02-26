using System;
using UnityEngine;
using DungeonSurvivor.Core.GridFunctionality;
using DungeonSurvivor.Controllers.Animations.Character;
using static DungeonSurvivor.Core.Events.GameplayEvents.Movement;

namespace DungeonSurvivor.Core.Player.Movement
{
    public class GridMovement : MonoBehaviour
    {
        [SerializeField] private CharacterAnimator characterAnimator;
        [SerializeField] private Vector2Int currentIndex;
        [SerializeField] private float moveTime;
        
        private Vector3 targetPosition;

        private void OnMoveInDirectionCalled(Vector2Int direction)
        {
            var block = GridManager.Instance.GetBlock(currentIndex + direction);

            if (!block) return;
            if (block.type is BlockType.None) return;
            if (Vector3.Distance(transform.position, targetPosition) > 0.01f) return;
            Move(block);
        }

        private void Move(Block block)
        {
            targetPosition = block.transform.position;
            targetPosition.y = 0.5f;
            currentIndex          = block.index;
            characterAnimator.running = true;
            transform.LookAt(targetPosition);
            LeanTween.move(gameObject, targetPosition, moveTime)
                .setEaseInSine()
                .setOnComplete(() => { characterAnimator.running = false; });
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