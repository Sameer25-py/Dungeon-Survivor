using System.Collections.Generic;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Timer;
using static DungeonSurvivor.Core.Events.GameplayEvents.Light;
using static DungeonSurvivor.Core.Events.GameplayEvents.Camera;

namespace DungeonSurvivor.Controllers.Animations.Lights
{
    public class DungeonLightController : MonoBehaviour
    {
        [SerializeField] private float       dungeonLitUpTimerProgress = 0.4f;
        [SerializeField] private List<Light> dungeonPointLights;
        [SerializeField] private float       defaultLightIntensity;
        [SerializeField] private float       minLightIntensity;

        private bool _isDungeonLitUp;

        private void LightUpDungeon()
        {
            for (int i = 0; i < dungeonPointLights.Count; i++)
            {
                int i1 = i;
                LeanTween.value(gameObject, minLightIntensity, defaultLightIntensity, 0.25f)
                    .setEase(LeanTweenType.easeShake)
                    .setDelay(0.5f * i)
                    .setOnUpdate(value =>
                    {
                        dungeonPointLights[i1]
                            .intensity = value;
                    })
                    .setOnComplete(() =>
                    {
                        dungeonPointLights[i1]
                            .intensity = defaultLightIntensity;
                        if (i1 == dungeonPointLights.Count - 1)
                        {
                            _isDungeonLitUp = true;
                        }
                    });
            }
        }

        private void DimDownDungeon(float dimFactor)
        {
            float adjustedIntensity = Mathf.Clamp(defaultLightIntensity - (defaultLightIntensity * dimFactor), minLightIntensity,
                defaultLightIntensity);
            for (int i = 0; i < dungeonPointLights.Count; i++)
            {
                int i1 = i;
                LeanTween.value(gameObject, dungeonPointLights[i1]
                        .intensity, adjustedIntensity, 0.5f)
                    .setEase(LeanTweenType.easeSpring)
                    .setOnUpdate(value =>
                    {
                        dungeonPointLights[i1]
                            .intensity = value;
                    })
                    .setOnComplete(() =>
                    {
                        if (adjustedIntensity <= minLightIntensity)
                        {
                            LeanTween.value(gameObject, minLightIntensity, 0f, 1f)
                                .setEase(LeanTweenType.easeOutElastic)
                                .setDelay(0.5f)
                                .setOnUpdate(
                                    (float value) =>
                                    {
                                        dungeonPointLights[i1]
                                            .intensity = value;
                                    });
                        }
                    });
            }
        }

        private void OnCountDownTimePassed(float progress)
        {
            if (progress == dungeonLitUpTimerProgress && !_isDungeonLitUp)
            {
                DungeonLightsLitUp?.Invoke();
                LightUpDungeon();
            }

            if (_isDungeonLitUp)
            {
                DimDownDungeon(progress);
            }
        }
        
        private void OnSwitchToMiniGameCameraCalled()
        {
            foreach (Light dungeonPointLight in dungeonPointLights)
            {
                dungeonPointLight.intensity = defaultLightIntensity;
            }
        }


        private void OnEnable()
        {
            CountDownTimePassed.AddListener(OnCountDownTimePassed);
            SwitchToMiniGameCamera.AddListener(OnSwitchToMiniGameCameraCalled);
            foreach (GameObject dungeonLightObj in GameObject.FindGameObjectsWithTag("DungeonLight"))
            {
                Light dungeonLight = dungeonLightObj.GetComponent<Light>();
                dungeonLight.intensity = minLightIntensity;
                dungeonPointLights.Add(dungeonLight);
            }
        }
        
        private void OnDisable()
        {
            CountDownTimePassed.RemoveListener(OnCountDownTimePassed);
            SwitchToMiniGameCamera.RemoveListener(OnSwitchToMiniGameCameraCalled);
        }
    }
}