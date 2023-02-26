using System;
using DungeonSurvivor.Core.Player.Movement;
using UnityEngine;

namespace DungeonSurvivor.Controllers.Animations.Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        public bool pushing;
        public bool running;
        
        [SerializeField] private Animator animator;
        [SerializeField] private float crossFadeTime;
        
        private int currentState;
        private float timeUntil;

        private static readonly int idle = Animator.StringToHash("DS_Char_Idle");
        private static readonly int run = Animator.StringToHash("DS_ Char_SimpleRun");
        private static readonly int push = Animator.StringToHash("DS_Char_Push");

        private void Update()
        {
            var state = GetStateHash();
            
            if (state == currentState) return;
            animator.CrossFade(state, crossFadeTime, 0);
            currentState = state;
        }
        
        private int GetStateHash()
        {
            if (Time.time < timeUntil) return currentState;

            // if (pushing) return LockState(push, movement.MoveTime);
            // return running ? LockState(run, movement.MoveTime) : idle;
            if (pushing) return push;
            if (running) return run;
            return idle;
        }
        
        private int LockState(int s, float t)
        {
            timeUntil = Time.time + t;
            return s;
        }
    }
}