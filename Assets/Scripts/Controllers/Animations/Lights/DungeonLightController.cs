using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonSurvivor.Controllers.Animations.Lights
{
    public class DungeonLightController : MonoBehaviour
    {
        [SerializeField] private List<Light> dungeonPointLights;
        [SerializeField] private float       defaultLightIntensity;
        [SerializeField] private float       minLightIntensity;

        private void OnEnable()
        {
            foreach (GameObject dungeonLight in GameObject.FindGameObjectsWithTag("DungeonLight"))
            {
                dungeonPointLights.Add(dungeonLight.GetComponent<Light>());
            }
        }

        private void Start()
        {
            dungeonPointLights.ForEach(lght => lght.intensity = minLightIntensity);
            LeanTween.value(gameObject, minLightIntensity, defaultLightIntensity, 0.5f)
                .setEase(LeanTweenType.easeOutElastic)
                .setDelay(1f)
                .setOnUpdate((float value) => { dungeonPointLights.ForEach(lght => lght.intensity = value); });
        }
    }
}