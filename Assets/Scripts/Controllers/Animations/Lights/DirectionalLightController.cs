using System;
using System.Resources;
using UnityEngine;
using UnityEngine.Serialization;
using static DungeonSurvivor.Core.Events.GameplayEvents.Light;
using static DungeonSurvivor.Core.Events.GameplayEvents.Timer;
using static DungeonSurvivor.Core.Events.GameplayEvents.Camera;

namespace DungeonSurvivor.Controllers.Animations.Lights
{
    public class DirectionalLightController : MonoBehaviour
    {
        [SerializeField] private float defaultIntensity     = 0.7f;
        [SerializeField] private float initialDimDownFactor = 0.5f;
        [SerializeField] private float timerDimDownFactor   = 0.05f;
        [SerializeField] private float miniGameIntensity    = 0.7f;

        private Light _directionalLight;
        private bool  _dimDownWithTimer;

        private void OnDugeonLightsLitUpCalled()
        {
            float dimDownIntensity = Mathf.Clamp(_directionalLight.intensity - initialDimDownFactor, 0f, defaultIntensity);
            LeanTween.value(_directionalLight.intensity, dimDownIntensity, 1f)
                .setEaseOutQuint()
                .setOnUpdate((float value) => { _directionalLight.intensity = value; })
                .setOnComplete(() => { _dimDownWithTimer                    = true; });
        }

        private void OnCountDownTimePassed(float progress)
        {
            if (_dimDownWithTimer)
            {
                float dimDownIntensity = Mathf.Clamp(_directionalLight.intensity - timerDimDownFactor, 0f, defaultIntensity);
                LeanTween.value(_directionalLight.intensity, dimDownIntensity, 1f)
                    .setEaseInQuint()
                    .setOnUpdate((float value) => { _directionalLight.intensity = value; });
            }
        }

        private void OnSwitchToMiniGameCameraCalled()
        {
            LeanTween.cancel(gameObject);
            LeanTween.value(_directionalLight.intensity, miniGameIntensity, 1.5f)
                .setEaseOutQuint()
                .setOnUpdate((float value) => { _directionalLight.intensity = value; });
        }
        
        private void OnEnable()
        {
            _directionalLight           = gameObject.GetComponent<Light>();
            _directionalLight.intensity = defaultIntensity;
            DungeonLightsLitUp.AddListener(OnDugeonLightsLitUpCalled);
            CountDownTimePassed.AddListener(OnCountDownTimePassed);
            SwitchToMiniGameCamera.AddListener(OnSwitchToMiniGameCameraCalled);
        }

        private void OnDisable()
        {
            DungeonLightsLitUp.RemoveListener(OnDugeonLightsLitUpCalled);
            CountDownTimePassed.RemoveListener(OnCountDownTimePassed);
            SwitchToMiniGameCamera.RemoveListener(OnSwitchToMiniGameCameraCalled);
        }
    }
}