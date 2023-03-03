using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Inventory;

public class Inventory : MonoBehaviour
{
    private const int                  _slots = 3;
    private       List<IInventoryItem> mItems = new List<IInventoryItem>();

    public void AddItem(IInventoryItem item)
    {
        if (mItems.Count < _slots)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                collider.enabled = false;
                mItems.Add(item);
                item.OnPickup();

                ItemAdded?.Invoke(item);
            }
        }
    }
}