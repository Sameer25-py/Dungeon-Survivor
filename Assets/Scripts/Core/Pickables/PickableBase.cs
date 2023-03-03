using System;
using DungeonSurvivor.Core.ID;
using UnityEngine;
using static DungeonSurvivor.Core.Events.GameplayEvents.Inventory;
using static DungeonSurvivor.Core.Events.Internal;

namespace DungeonSurvivor.Core.Pickables
{
    public class PickableBase : MonoBehaviour
    {
        public Vector2Int   CurrentIndex;
        public PickableData PickableData;
        public int          ID;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                AddItem?.Invoke(ID, PickableData);
            }
        }

        private void OnItemAddedInInventory(int id)
        {
            if (ID == id)
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            Vector3 position = transform.position;
            CurrentIndex.x = (int)position.x;
            CurrentIndex.y = (int)position.z;

            ItemAddedSuccessfully.AddListener(OnItemAddedInInventory);
        }

        private void Start()
        {
            ID = IDManager.AssignPickableID();
        }

        private void OnDisable()
        {
            ItemAddedSuccessfully.RemoveListener(OnItemAddedInInventory);
        }

        private void OnDestroy()
        {
            PickableDestroyed?.Invoke(this);
        }
    }

    [Serializable]
    public enum PickableType
    {
        None,
        BlueGem,
        RedGem,
        GreenGem
    }

    [Serializable]
    public class PickableData
    {
        public Sprite       UISprite;
        public PickableType type;
    }
}