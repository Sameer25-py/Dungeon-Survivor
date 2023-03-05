using System.Collections.Generic;
using DungeonSurvivor.Controllers.Animations.UI;
using UnityEngine;
using UnityEngine.UI;
using static DungeonSurvivor.Core.Events.Internal;
using static DungeonSurvivor.Core.Events.GameplayEvents.Inventory;

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
                .AnimateAdd();

            _currentIndex++;
        }

        private void OnAddItemFailedCalled()
        {
            foreach (Image inventoryItem in inventoryItems)
            {
                inventoryItem.GetComponent<InventoryAddition>()
                    .AnimateFail();
            }
        }

        private void OnEnable()
        {
            UpdateInventoryUI.AddListener(OnUpdateInventoryUIcalled);
            AddItemFailed.AddListener(OnAddItemFailedCalled);
        }

        private void OnDisable()
        {
            UpdateInventoryUI.RemoveListener(OnUpdateInventoryUIcalled);
            AddItemFailed.RemoveListener(OnAddItemFailedCalled);
        }
    }
}