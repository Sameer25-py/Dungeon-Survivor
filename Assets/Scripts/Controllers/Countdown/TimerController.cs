using System;
using DungeonSurvivor.Core.Timer;
using TMPro;
using UnityEngine;

namespace DungeonSurvivor.Controllers.Countdown
{
    public class TimerController : MonoBehaviour
    {
        [Header("CountDown parameters")] [SerializeField]
        private int seconds = 180;

        [SerializeField] private float     tickRate = 1f;
        [SerializeField] private CountDown countDown;
        [SerializeField] private TMP_Text  levelEndCountDownText;

        private void ObserveTimer()
        {
            CountDownTime countDownTimer  = countDown.GetCountDownTime;
            string        countDownString = $"{countDownTimer.Minutes:00} : {countDownTimer.Seconds:00}";
            levelEndCountDownText.text = countDownString;

            //TODO: Insert logic to broadcast progress
        }

        private void Start()
        {
            countDown.StartCountdown(seconds, tickRate);
        }

        private void Update()
        {
            ObserveTimer();
        }
    }
}