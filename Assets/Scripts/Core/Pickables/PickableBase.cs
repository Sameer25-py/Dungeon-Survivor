using System;
using DungeonSurvivor.Core.ID;
using DungeonSurvivor.Core.Player.InventoryFunctionality;
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
                GetComponent<Collider>()
                    .enabled = false;
                Animate();
            }
        }

        private void Animate()
        {
            float time = 0f;
            LeanTween.scale(gameObject, Vector3.zero, 0.2f)
                .setEaseInCirc();
            LeanTween.move(gameObject, Inventory.Instance.PlayerBackPack, 0.2f)
                .setEaseOutCubic()
                .setOnComplete(() => { Destroy(gameObject); });
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
            LeanTween.rotateAround(gameObject, Vector3.up, 360f, 5f).setLoopClamp().setEaseLinear();
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