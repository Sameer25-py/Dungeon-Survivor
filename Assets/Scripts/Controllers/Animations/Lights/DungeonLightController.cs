using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using static DungeonSurvivor.Core.Events.GameplayEvents.Timer;

namespace DungeonSurvivor.Controllers.Animations.Lights
{
    public class DungeonLightController : MonoBehaviour
    {
        [SerializeField] private List<Light> dungeonPointLights;
        [SerializeField] private float       defaultLightIntensity;
        [SerializeField] private float       minLightIntensity;

        private void LightUpDungeon()
        {
            for (int i = 0; i < dungeonPointLights.Count; i++)
            {
                int i1 = i;
                LeanTween.value(gameObject, minLightIntensity, defaultLightIntensity, 0.5f)
                    .setEase(LeanTweenType.easeOutElastic)
                    .setDelay(0.1f)
                    .setDelay(0.5f * i)
                    .setOnUpdate(value =>
                    {
                        dungeonPointLights[i1]
                            .intensity = value;
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
            DimDownDungeon(progress);
        }

        private void OnEnable()
        {
            CountDownTimePassed.AddListener(OnCountDownTimePassed);
            foreach (GameObject dungeonLightObj in GameObject.FindGameObjectsWithTag("DungeonLight"))
            {
                Light dungeonLight = dungeonLightObj.GetComponent<Light>();
                dungeonLight.intensity = minLightIntensity;
                dungeonPointLights.Add(dungeonLight);
            }
        }

        private void Start()
        {
            LightUpDungeon();
        }

        private void OnDisable()
        {
            CountDownTimePassed.RemoveListener(OnCountDownTimePassed);
        }
    }
}