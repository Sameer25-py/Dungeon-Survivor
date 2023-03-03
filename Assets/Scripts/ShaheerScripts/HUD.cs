using System;
using UnityEngine;
using UnityEngine.UI;
using static DungeonSurvivor.Core.Events.GameplayEvents.Inventory;


public class HUD : MonoBehaviour
{
    private int       i = 0;

    private void OnItemAdded(IInventoryItem item)
    {
        Transform inventoryPanel = transform.Find("Inventory");
        foreach (Transform slot in inventoryPanel)
        {
            Image image = slot.GetChild(0)
                .GetChild(0)
                .GetComponent<Image>();
            if (image.enabled == false)
            {
                print("getting in slot");
                image.enabled = true;
                image.sprite  = item.Image;
                print(i);
                i = i + 1;
                break;
            }
        }
    }

    private void OnEnable()
    {
        ItemAdded.AddListener(OnItemAdded);
    }

    private void OnDisable()
    {
        ItemAdded.RemoveListener(OnItemAdded);
    }
}