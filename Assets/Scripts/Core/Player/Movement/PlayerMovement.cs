using System;
using DungeonSurvivor.Analytics.Player;
using UnityEngine;
using DungeonSurvivor.Controllers.Animations.Character;
using DungeonSurvivor.Core.GridFunctionality;

namespace DungeonSurvivor.Core.Player.Movement
{
    public class PlayerMovement : GridMovement
    {
        [SerializeField] protected CharacterAnimator characterAnimator;
        
        private void Start()
        {
            ID = 0; // Constant for player input
        }

        protected override void Move(Block block)
        {
            base.Move(block);
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
        }
        
        protected override void Animate()
        {
            transform.LookAt(targetPosition);
            characterAnimator.running = true;
            LeanTween.move(gameObject, targetPosition, moveTime)
                .setEaseInSine();
        }

    }
}