using System;
using System.Collections.Generic;
using DungeonSurvivor.Controllers.Animations.UI;
using UnityEngine;
using UnityEngine.UI;
using static DungeonSurvivor.Core.Events.Internal;

namespace DungeonSurvivor.Controllers.Inventory
{
    public class InventoryUIController : MonoBehaviour
    {
        [SerializeField] private List<Image> inventoryItems;
        private                  int         _currentIndex = 0;

        private void OnUpdateInventoryUIcalled(Sprite sprite)
        {
            inventoryItems[_currentIndex]
                .sprite = sprite;

            inventoryItems[_currentIndex]
                .gameObject.GetComponent<InventoryAddition>()
                .Animate();

            _currentIndex++;
        }


        private void OnEnable()
        {
            UpdateInventoryUI.AddListener(OnUpdateInventoryUIcalled);
        }

        private void OnDisable()
        {
            UpdateInventoryUI.RemoveListener(OnUpdateInventoryUIcalled);
        }
    }
}