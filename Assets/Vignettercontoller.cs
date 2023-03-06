using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static DungeonSurvivor.Core.Events.GameplayEvents.Light;

namespace DefaultNamespace
{
    public class Vignettercontoller : MonoBehaviour
    {
        [SerializeField] private Volume GlobalVolume;

        private Vignette _vignette;

        private void OnDungeonLightsLitUp()
        {
            
        }

        private void OnEnable()
        {
            _vignette        = GlobalVolume.profile.TryGet(out Vignette x) ? x : null;
            _vignette.active = false;
            DungeonLightsLitUp.AddListener(OnDungeonLightsLitUp);
        }

        private void OnDisable()
        {
            DungeonLightsLitUp.RemoveListener(OnDungeonLightsLitUp);
        }
    }
}