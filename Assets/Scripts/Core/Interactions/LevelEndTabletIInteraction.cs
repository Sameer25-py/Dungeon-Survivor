using System;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Camera;
using static DungeonSurvivor.Core.Events.Internal;

namespace DungeonSurvivor.Core.Interactions
{
    public class LevelEndTabletIInteraction : MonoBehaviour
    {
        private bool _broadcastInteractionOnce;
        private bool _isInteractionAvailable = true;

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

        private void OnEnable()
        {
            EnableLevelEnd.AddListener(OnEnableLevelEndCalled);
        }

        private void OnDisable()
        {
            EnableLevelEnd.RemoveListener(OnEnableLevelEndCalled);
        }
    }
}