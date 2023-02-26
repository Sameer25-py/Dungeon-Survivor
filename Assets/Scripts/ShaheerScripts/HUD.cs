
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class HUD : MonoBehaviour
    {
      private int i =0;
      public Inventory Inventory;
      void Start(){
        Inventory.ItemAdded += InventoryScript_ItemAdded;
      }
      private void InventoryScript_ItemAdded(object sender,InventoryEventArgs e){
        Transform inventoryPanel = transform.Find("Inventory");
        foreach (Transform slot in inventoryPanel)
        {
             Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            if(image.enabled==false){
              print("getting in slot");
              image.enabled=true;
               image.sprite = e.Item.Image;
               print(i);
               i=i+1;
               break;
            }
            
        }
      }
    }

