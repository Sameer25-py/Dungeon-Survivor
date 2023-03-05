using System;
using System.Collections.Generic;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Camera;

namespace DungeonSurvivor.Controllers.UI
{
    public class GameplayUIController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> gameplayUIElements;

        private void EnableGameplayUI()
        {
            foreach (GameObject uiElement in gameplayUIElements)
            {
                uiElement.SetActive(true);
            }
        }

        private void DisableGameplayUI()
        {
            foreach (GameObject uiElement in gameplayUIElements)
            {
                uiElement.SetActive(false);
            }
        }

        private void OnEnable()
        {
            SwitchToMiniGameCamera.AddListener(DisableGameplayUI);
        }

        private void OnDisable()
        {
            SwitchToMiniGameCamera.RemoveListener(DisableGameplayUI);
        }
    }
}