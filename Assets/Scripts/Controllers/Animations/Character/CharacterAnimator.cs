using UnityEngine;

namespace DungeonSurvivor.Controllers.Animations.Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        public bool pushing;
        public bool running;
        public AudioSource asource;
        [SerializeField] private Animator animator;
        [SerializeField] private float crossFadeTime;
        
        private int currentState;
        private float timeUntil;

        private static readonly int idle = Animator.StringToHash("DS_Char_Idle");
        private static readonly int run = Animator.StringToHash("DS_ Char_SimpleRun");
        private static readonly int run2 = Animator.StringToHash("DS_Char_NarutoRun");
        private static readonly int push = Animator.StringToHash("DS_Char_Push");

        private static readonly int dance1 = Animator.StringToHash("DS_Char_HiphopDance1");
        private static readonly int dance2 = Animator.StringToHash("DS_Char_HiphopDance2");
        private static readonly int dance3 = Animator.StringToHash("DS_Char_HiphopDance3");
        private static readonly int dance4 = Animator.StringToHash("DS_Char_HiphopDance4");
        private static readonly int dance5 = Animator.StringToHash("DS_Char_HiphopDance5");
        private static readonly int dance6 = Animator.StringToHash("DS_Char_HiphopDance6");
        
        private void Update()
        {
            var state = GetStateHash();
            if (state == currentState) return;
            if (state == run2)
            {
                asource.Play();
            }
            else
            {
                asource.Stop();
            }
            animator.CrossFade(state, crossFadeTime);
            currentState = state;
        }
        
        private int GetStateHash()
        {
            if (Time.time < timeUntil) return currentState;

            if (pushing) return push;
            if (running) return run2;
            return idle;
        }
        
        // private int LockState(int s, float t)
        // {
        //     timeUntil = Time.time + t;
        //     return s;
        // }
    }
}