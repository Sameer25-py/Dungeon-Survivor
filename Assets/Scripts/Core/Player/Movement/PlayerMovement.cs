using System;
using UnityEngine;
using DungeonSurvivor.Controllers.Animations.Character;

namespace DungeonSurvivor.Core.Player.Movement
{
    public class PlayerMovement : GridMovement
    {
        [SerializeField] protected CharacterAnimator characterAnimator;
        
        private void Start()
        {
            ID = 0; // Constant for player input
        }

        protected override void Update()
        {
            base.Update();
            if (timeSinceInput > exitTime && characterAnimator.running)
            {
                characterAnimator.running = false;
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