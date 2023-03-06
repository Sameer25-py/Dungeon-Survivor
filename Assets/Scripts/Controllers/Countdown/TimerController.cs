using System;
using System.Collections.Generic;
using System.Linq;
using DungeonSurvivor.Core.Managers;
using DungeonSurvivor.Core.Timer;
using TMPro;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Timer;

namespace DungeonSurvivor.Controllers.Countdown
{
    public class TimerController : MonoBehaviour
    {
        [Header("CountDown parameters")] [SerializeField]
        private int seconds = 180;

        [SerializeField] private float     tickRate = 1f;
        [SerializeField] private CountDown countDown;
        [SerializeField] private TMP_Text  levelEndCountDownText;

        [SerializeField] private List<TimerEvents> TimerEvents;

        private void ObserveTimer()
        {
            CountDownTime countDownTimer  = countDown.GetCountDownTime;
            string        countDownString = $"{countDownTimer.Minutes:00} : {countDownTimer.Seconds:00}";
            levelEndCountDownText.text = countDownString;

            foreach (TimerEvents timerEvent in TimerEvents.Where(timerEvent =>
                         countDownTimer.CountDownProgress >= timerEvent.TriggerThreshold && !timerEvent.IsTriggeredOnce))
            {
                timerEvent.IsTriggeredOnce = true;
                CountDownTimePassed?.Invoke(countDownTimer.CountDownProgress);
            }

            if (countDownTimer.CountDownProgress >= 1f)
            {
                CountDownEnded?.Invoke();
                GameManager.Instance.LoadScene("Scenes/Shaheer/Revamp/Lobby");
            }
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

    [Serializable]
    public class TimerEvents
    {
        public float TriggerThreshold;
        public bool  IsTriggeredOnce;
    }
}