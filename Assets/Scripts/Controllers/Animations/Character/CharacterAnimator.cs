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
        private static readonly int run2 = Animator.StringToHash("DS_Char_NarutoRun");
        private static readonly int push = Animator.StringToHash("DS_Char_Push");

        private void Update()
        {
            var state = GetStateHash();
            print($"GetState={state}, CurrentState={currentState}");
            
            if (state == currentState) return;
            animator.CrossFade(state, crossFadeTime);
            currentState = state;
        }
        
        private int GetStateHash()
        {
            if (Time.time < timeUntil) return currentState;

            // if (pushing) return LockState(push, 0.15f);
            // return running ? LockState(run, movement.MoveTime) : idle;
            if (pushing) return push;
            if (running) return run2;
            return idle;
        }
        
        private int LockState(int s, float t)
        {
            timeUntil = Time.time + t;
            return s;
        }
    }
}