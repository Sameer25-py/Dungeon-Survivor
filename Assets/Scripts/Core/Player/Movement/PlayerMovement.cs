using System;
using DungeonSurvivor.Analytics.Player;
using UnityEngine;
using DungeonSurvivor.Controllers.Animations.Character;
using DungeonSurvivor.Core.GridFunctionality;
using DungeonSurvivor.Core.Pushables;

namespace DungeonSurvivor.Core.Player.Movement
{
    public class PlayerMovement : GridMovement
    {
        
        [SerializeField] protected CharacterAnimator characterAnimator;
        private                    bool              _isInteractWithPushable;
        private                    float             _pushTime = 0.4f;

        private void Start()
        {
            ID = 0; // Constant for player input
        }

        private bool CheckForPushableInteraction(Vector2Int index)
        {
            Tuple<bool, PushableBase> pushable = GridManager.Instance.CheckForPushable(index);
            return pushable.Item1;
        }

        protected override void Move(Block block, Vector2Int direction, float time = 0f)
        {
            Tuple<bool, PushableBase> pushable = GridManager.Instance.CheckForPushable(currentIndex + direction);
            if (pushable.Item1)
            {
                _isInteractWithPushable = true;
                time                    = _pushTime;
                if (!pushable.Item2.ApplyPush(direction))
                {
                    _isInteractWithPushable = false;
                    return;
                }
            }
            else
            {
                _isInteractWithPushable = false;
                time                    = moveTime;
            }

            base.Move(block, direction, time);
            PlayerDataHandler.PlayerMoveCountPerStage += 1;
        }

        protected override void Update()
        {
            base.Update();
            if (timeSinceInput > exitTime && characterAnimator.running)
            {
                characterAnimator.running = false;
                characterAnimator.pushing = false;
               
            }
            else if (timeSinceInput > exitTime && characterAnimator.pushing)
            {
                characterAnimator.pushing = true;
                characterAnimator.running = false;
                
            }
          

        }


        protected override void Animate(float time)
        {
            transform.LookAt(targetPosition);
            if (_isInteractWithPushable)
            {
                characterAnimator.running = false;
                characterAnimator.pushing = true;
            }
            else
            {
                characterAnimator.running = true;
                characterAnimator.pushing = false;
            }

            LeanTween.move(gameObject, targetPosition, time)
                .setEaseInSine();
        }
    }
}