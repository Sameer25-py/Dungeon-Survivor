using System;
using DungeonSurvivor.Core.GridFunctionality;
using UnityEngine;

namespace DungeonSurvivor.Core.Pushables
{
    public class PushableBase : MonoBehaviour
    {
        public                     Vector2Int CurrentIndex;
        public                     int        PushFactor = 1;
        [SerializeField] protected float      MoveSpeed  = 0.3f;

        public bool ApplyPush(Vector2Int direction)
        {
            int adjustedFactor = PushFactor;
            for (int i = 1; i <= PushFactor; i++)
            {
                Vector2Int nextIndex = CurrentIndex + direction * i;
                Tuple<bool, PushableBase> checkForPushable =
                    GridManager.Instance.IsIndexCollideWithPushable(nextIndex);
                Block blk = GridManager.Instance.GetBlock(nextIndex);
                
                if (checkForPushable.Item1 && i == 1) return false;
                if (!blk                   && i == 1) return false;
                if (blk && !checkForPushable.Item1) continue;
                
                adjustedFactor = i - 1;
                break;
            }

            Block block = GridManager.Instance.GetBlock(CurrentIndex + direction * adjustedFactor);
            if (!block)
            {
                return false;
            }

            Vector3 targetPosition = block.transform.position;
            targetPosition.y = 0.8f;
            CurrentIndex     = block.index;
            OnPushApplied(direction, targetPosition);
            return true;
        }

        protected virtual void OnPushApplied(Vector2Int direction, Vector3 targetPosition)
        {
            LeanTween.move(gameObject, targetPosition, MoveSpeed)
                .setEaseInCubic();
        }

        private void OnEnable()
        {
            Vector3 position = transform.position;
            CurrentIndex.x = (int)position.x;
            CurrentIndex.y = (int)position.z;
        }
    }
}