using System;
using DungeonSurvivor.Core.Events;
using DungeonSurvivor.Core.Managers;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Camera;
using static DungeonSurvivor.Core.Events.Internal;

namespace DungeonSurvivor.Core.Interactions
{
    public class LevelEndTabletIInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject book;
        private                  bool       _broadcastInteractionOnce;
        private                  bool       _isInteractionAvailable;


        private void OnTriggerEnter(Collider other)
        {
            if (!_isInteractionAvailable) return;
            if (!other.CompareTag("Player")) return;
            if (_broadcastInteractionOnce) return;
            _broadcastInteractionOnce = true;
            SwitchToMiniGameCamera?.Invoke();
        }

        private void OnEnableLevelEndCalled()
        {
            _isInteractionAvailable = true;
        }

        private void PlayLadderUpAnimation()
        {
            LeanTween.rotateAround(book, Vector3.up, 360f, 3f)
                .setEaseOutCirc();
            LeanTween.moveLocalY(book, 3f, 3f)
                .setEaseOutCirc()
                .setOnComplete(() =>
                {
                    Light[] lights = FindObjectsOfType<Light>();
                    foreach (Light light1 in lights)
                    {
                        LeanTween.value(gameObject, light1.intensity, 0f, 1f)
                            .setDelay(1.5f)
                            .setOnUpdate((float value) => { light1.intensity = value; })
                            .setEaseInElastic().setOnComplete(() =>
                            {
                                GameManager.Instance.LoadScene("Scenes/Shaheer/Revamp/Lobby");
                            });
                        
                    }
                });
        }

        private void OnSwitchToEndLevelCameraCalled()
        {
            Invoke(nameof(PlayLadderUpAnimation), 2f);
        }

        private void OnEnable()
        {
            EnableLevelEnd.AddListener(OnEnableLevelEndCalled);
            SwitchToEndLevelCamera.AddListener(OnSwitchToEndLevelCameraCalled);
        }

        private void OnDisable()
        {
            EnableLevelEnd.RemoveListener(OnEnableLevelEndCalled);
            SwitchToEndLevelCamera.RemoveListener(OnSwitchToEndLevelCameraCalled);
        }
    }
}