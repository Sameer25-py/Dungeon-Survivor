using System;
using DungeonSurvivor.Analytics.Player;
using DungeonSurvivor.Core.GridFunctionality;
using DungeonSurvivor.Core.Pushables;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Movement;

namespace DungeonSurvivor.Core.Player.Controllers
{
    public class GridPlayerController : MonoBehaviour
    {
        [SerializeField] private Vector2Int currentIndex;
        [SerializeField] private Vector3    targetPosition;
        private                  bool       _isMoving;

        private void OnMoveInDirectionCalled(Vector2Int direction)
        {
            if (_isMoving) return;
            Tuple<bool, PushableBase> checkForPushable =
                GridManager.Instance.IsIndexCollideWithPushable(currentIndex + direction);
            if (checkForPushable.Item1)
            {
                bool isPushApplied = checkForPushable.Item2.ApplyPush(direction);
                if (isPushApplied)
                {
                    Move(direction);
                }
            }
            else
            {
                Move(direction);
            }
        }
        private void Move(Vector2Int direction)
        {
            Block block = GridManager.Instance.GetBlock(currentIndex + direction);
            if (!block) return;
            targetPosition   = block.transform.position;
            targetPosition.y = 1f;
            _isMoving        = true;
            LeanTween.move(gameObject, targetPosition, 0.3f)
                .setEaseInSine()
                .setOnComplete(() =>
                {
                    currentIndex = block.index;
                    _isMoving    = false;
                });
            PlayerDataHandler.PlayerMoveCountPerStage += 1;
        }
        private void OnEnable()
        {
            MoveInDirection.AddListener(OnMoveInDirectionCalled);
            targetPosition = transform.position;
            currentIndex   = new Vector2Int((int)targetPosition.x, (int)targetPosition.z);
        }
        private void OnDisable()
        {
            MoveInDirection.RemoveListener(OnMoveInDirectionCalled);
        }
    }
}