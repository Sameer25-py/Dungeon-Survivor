using System.Collections.Generic;
using DungeonSurvivor.Core.Managers;
using DungeonSurvivor.Core.Pickables;
using static DungeonSurvivor.Core.Events.GameplayEvents.Inventory;
using static DungeonSurvivor.Core.Events.Internal;

namespace DungeonSurvivor.Core.Player.InventoryFunctionality
{
    public class Inventory : Singleton<Inventory>
    {
        public int                InventoryCapacity = 3;
        public List<PickableData> Pickables;

        private void OnAddItemCalled(int id, PickableData pickableData)
        {
            if (Pickables.Count != InventoryCapacity)
            {
                Pickables.Add(new PickableData()
                {
                    type     = pickableData.type,
                    UISprite = pickableData.UISprite
                });
                UpdateInventoryUI?.Invoke(pickableData.UISprite);
                ItemAddedSuccessfully?.Invoke(id);
            }
            else
            {
                AddItemFailed?.Invoke();
            }
        }

        private void OnEnable()
        {
            AddItem.AddListener(OnAddItemCalled);
        }

        private void OnDisable()
        {
            AddItem.RemoveListener(OnAddItemCalled);
        }
    }
}