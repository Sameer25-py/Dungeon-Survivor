using System;
using DungeonSurvivor.Core.GridFunctionality;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static DungeonSurvivor.Core.Events.GameplayEvents.Light;
using static DungeonSurvivor.Core.Events.GameplayEvents.Timer;
using static DungeonSurvivor.Core.Events.GameplayEvents.Camera;

namespace DefaultNamespace
{
    public class Vignettercontoller : MonoBehaviour
    {
        [SerializeField] private Volume GlobalVolume;

        private Vignette _vignette;
        private bool     _isPingPongStart;

        private float _sineWaveDelay = 0.7f;

        private void OnDungeonLightsLitUp()
        {
            _vignette.active = true;
            LeanTween.value(gameObject, 0f, 0.2f, 1f)
                .setEaseInElastic()
                .setOnUpdate((float value) => { _vignette.intensity.value = value; })
                .setOnComplete(() =>
                {
                    LeanTween.value(gameObject, _vignette.intensity.value, 0.8f, _sineWaveDelay)
                        .setLoopPingPong(-1)
                        .setEaseOutBounce()
                        .setOnUpdate(
                            (float value) => { _vignette.intensity.value = value; });
                });
        }

        private void ResetVignette()
        {
            LeanTween.value(gameObject, _vignette.intensity.value, 0f, 1f)
                .setEaseOutQuint()
                .setOnUpdate(
                    (float value) => { _vignette.intensity.value = value; })
                .setOnComplete(() => { _vignette.active          = false; });
        }


        private void OnCountDownTimePassed(float progress)
        {
            _sineWaveDelay = 1f - progress / 2f;
            if (progress == 1f)
            {
                LeanTween.cancel(gameObject);
                ResetVignette();
            }
        }
        
        private void OnSwitchToMiniGameCameraCalled()
        {   
            LeanTween.cancel(gameObject);
            ResetVignette();
        }


        private void OnEnable()
        {
            _vignette        = GlobalVolume.profile.TryGet(out Vignette x) ? x : null;
            _vignette.active = false;
            DungeonLightsLitUp.AddListener(OnDungeonLightsLitUp);
            CountDownTimePassed.AddListener(OnCountDownTimePassed);
            SwitchToMiniGameCamera.AddListener(OnSwitchToMiniGameCameraCalled);
        }
        

        private void OnDisable()
        {
            DungeonLightsLitUp.RemoveListener(OnDungeonLightsLitUp);
            CountDownTimePassed.RemoveListener(OnCountDownTimePassed);
            SwitchToMiniGameCamera.RemoveListener(OnSwitchToMiniGameCameraCalled);
        }
    }
}