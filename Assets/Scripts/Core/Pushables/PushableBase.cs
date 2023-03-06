using System;
using System.Collections;
using DungeonSurvivor.Controllers.Animations.Objects;
using DungeonSurvivor.Core.GridFunctionality;
using DungeonSurvivor.Core.Managers;
using DungeonSurvivor.Core.Player;
using static DungeonSurvivor.Core.Events.Internal;
using UnityEngine;

namespace DungeonSurvivor.Core.Pushables
{
    public class PushableBase : MonoBehaviour
    {
        public AudioSource asource;
        public                     PushableType PushableType;
        public                     Vector2Int   CurrentIndex;
        public                     int          PushFactor = 1;
        [SerializeField] protected float        MoveSpeed  = 0.3f;
        [SerializeField] protected PushableType PassThroughPushableType;

        protected bool      IsMoving;
        private   Coroutine _pushCoroutine;

        protected virtual bool CheckNextIndexInDirectionMoveable(Vector2Int currentIndex, Vector2Int direction, PushableType
            passThroughPushable)
        {
            Vector2Int nextIndexIndirection = currentIndex + direction;
            Block      block                = GridManager.Instance.GetBlock(nextIndexIndirection);
            if (!block || block.type is BlockType.Wall || GridManager.Instance.CheckForPickable(nextIndexIndirection))
                return false;
            Tuple<bool, PushableBase> checkForPushable =
                GridManager.Instance.CheckForPushable(nextIndexIndirection);
            if (checkForPushable.Item1)
            {
                if (passThroughPushable != PushableType.None && checkForPushable.Item2.PushableType == passThroughPushable)
                {
                    checkForPushable.Item2.OnPushablePassedThrough();
                    return true;
                }

                return false;
            }

            return true;
        }

        private IEnumerator Push(Vector2Int direction)
        {
            int totalBlocksMoved = 0;
            while (true)
            {
                if (IsMoving)
                {
                    yield return new WaitForEndOfFrame();
                    asource.Play();
                    continue;
                }

                if (totalBlocksMoved == PushFactor)
                {
                    yield break;
                }

                if (CheckNextIndexInDirectionMoveable(CurrentIndex, direction, PassThroughPushableType))
                {
                    Block blk = GridManager.Instance.GetBlock(CurrentIndex + direction);
                    totalBlocksMoved += 1;
                    Vector3 targetPosition = blk.transform.position;
                    targetPosition.y = 0.8f;
                    CurrentIndex     = blk.index;

                    OnPushApplied(targetPosition, direction);
                }
                else
                {
                    if (_pushCoroutine != null)
                    {
                        StopCoroutine(_pushCoroutine);
                        asource.Pause();
                    }
                    yield return null;
                }

                yield return new WaitForEndOfFrame();
            }
        }

        public bool ApplyPush(Vector2Int direction)
        {
            if (CheckNextIndexInDirectionMoveable(CurrentIndex, direction, PassThroughPushableType))
            {
                if (_pushCoroutine is not null)
                {
                    StopCoroutine(_pushCoroutine);
                }

                LeanTween.cancel(gameObject);
                if (PushFactor == -1)
                {
                    Vector2Int gridSize = LevelManager.Instance.GetGridSize();
                    PushFactor = Math.Max(gridSize.x, gridSize.y);
                }

                if (_pushCoroutine is not null)
                {
                    StopCoroutine(_pushCoroutine);
                }

                _pushCoroutine = StartCoroutine(Push(direction));
                return true;
            }

            return false;
        }

        protected virtual void OnPushApplied(Vector3 targetPosition, Vector2Int direction)
        {
            IsMoving = true;
            LeanTween.move(gameObject, targetPosition, MoveSpeed)
                .setEaseLinear()
                .setOnComplete(() =>
                {
                    IsMoving = false;
                    Block currentBlock = GridManager.Instance.GetBlock(CurrentIndex);
                    if (currentBlock.type == BlockType.Water)
                    {
                        StopCoroutine(_pushCoroutine);
                        ChangeBlockType?.Invoke(CurrentIndex, BlockType.Standing);
                        LeanTween.cancel(gameObject);
                        PushableDestroyed?.Invoke(this);
                        gameObject.AddComponent<FloatingAnimation>();
                        enabled = false;
                    }
                });
        }

        protected virtual void OnPushablePassedThrough()
        {
            Destroy(gameObject);
        }

        private void OnEnable()
        {
            Vector3 position = transform.position;
            CurrentIndex.x = (int)position.x;
            CurrentIndex.y = (int)position.z;
        }

        protected virtual void OnDestroy()
        {
            PushableDestroyed?.Invoke(this);
        }
    }

    [Serializable]
    public enum PushableType
    {
        None,
        WoodenBox,
        SteelBox,
        Boulder
    }
}