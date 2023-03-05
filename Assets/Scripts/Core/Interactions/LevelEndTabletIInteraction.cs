using System;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Camera;
using static DungeonSurvivor.Core.Events.Internal;

namespace DungeonSurvivor.Core.Interactions
{
    public class LevelEndTabletIInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject book;
        private                  bool       _broadcastInteractionOnce;
        private                  bool       _isInteractionAvailable = true;


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
                .setEaseOutQuint();
        }
        
        private void OnSwitchToEndLevelCameraCalled()
        {
            Invoke(nameof(PlayLadderUpAnimation),2f);
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