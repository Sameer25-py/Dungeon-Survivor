using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonSurvivor.Scenes.Rafay
{
    public class LobbyController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float animationChangeTime;
        [SerializeField] private float crossFadeTime;

        private float timeSinceDanceStarted;
        private static readonly List<int> dances= new()
        {
            Animator.StringToHash("DS_Char_HipHopDance1"),
            Animator.StringToHash("DS_Char_HipHopDance2"),
            Animator.StringToHash("DS_Char_HipHopDance3"),
            Animator.StringToHash("DS_Char_HipHopDance4"),
            Animator.StringToHash("DS_Char_HipHopDance5"),
            Animator.StringToHash("DS_Char_HipHopDance6")
        };
        
        private void Start()
        {
            timeSinceDanceStarted = animationChangeTime;
        }

        private void Update()
        {
            timeSinceDanceStarted += Time.deltaTime;
            if (timeSinceDanceStarted < animationChangeTime) return;
            animator.CrossFade(dances[Random.Range(0, dances.Count)], crossFadeTime, 0);
            timeSinceDanceStarted = 0f;
        }
    }
}
