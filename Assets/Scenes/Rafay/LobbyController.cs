using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonSurvivor.Scenes.Rafay
{
    public class LobbyController : MonoBehaviour
    {
        [SerializeField] private TMP_Text message;
        [SerializeField] private Animator animator;
        [SerializeField] private float animationChangeTime;
        [SerializeField] private float crossFadeTime;

        private float timeSinceDanceStarted;
        private const string NICK = "Nick"; 
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
            message.text = $"Welcome {PlayerPrefs.GetString(NICK)}!";
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
