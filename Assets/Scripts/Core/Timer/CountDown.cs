using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DungeonSurvivor.Core.Timer
{
    [Serializable]
    public struct CountDownTime
    {
        public  int   Minutes;
        public  int   Seconds;
        public float CountDownProgress;

        public CountDownTime(int minutes, int seconds, float progress)
        {
            Minutes           = minutes;
            Seconds           = seconds;
            CountDownProgress = progress;
        }
    }

    public class CountDown : MonoBehaviour
    {
        private float _totalSeconds;
        private float _tickRate;
        private float _remainingTime;
        private bool  _isCountDownInProgress;
        private float _elapsedTime;

        public CountDownTime GetCountDownTime { get; private set; }

        private void CalculateTick()
        {
            _remainingTime -= 1f;
            int minutes = Mathf.FloorToInt(_remainingTime / 60f);
            int seconds = Mathf.FloorToInt(_remainingTime % 60f);
            GetCountDownTime = new(minutes, seconds, 1f - _remainingTime / _totalSeconds);
            if (_remainingTime == 0f)
            {
                StopCountDown();
            }
        }

        public void StartCountdown(int seconds, float tickRate)
        {
            _isCountDownInProgress = true;
            _totalSeconds          = seconds;
            _tickRate              = tickRate;
            _remainingTime         = _totalSeconds;
        }

        public void PauseCountDown()
        {
            _isCountDownInProgress = false;
        }

        public void StopCountDown()
        {
            _isCountDownInProgress = false;
            _remainingTime         = 0f;
            _totalSeconds          = 0f;
            _remainingTime         = 0f;
        }

        private void Update()
        {
            if (_isCountDownInProgress)
            {
                _elapsedTime += Time.deltaTime;
                if (!(_elapsedTime >= _tickRate)) return;
                _elapsedTime = 0f;
                CalculateTick();
            }
        }
    }
}