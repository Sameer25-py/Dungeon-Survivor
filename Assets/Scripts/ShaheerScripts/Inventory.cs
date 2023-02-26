using System.Net.Http.Headers;
using System.Data;
using System.Runtime.CompilerServices;
using System.Resources;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    public class Inventory : MonoBehaviour
    {   
        private const int _slots=3;
        private List<IInventoryItem> mItems =new List<IInventoryItem>();
        public event EventHandler<InventoryEventArgs> ItemAdded;
        public void AddItem(IInventoryItem item)
        {
          if(mItems.Count < _slots)
          {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if(collider.enabled){
                collider.enabled = false;
                mItems.Add(item);
                item.OnPickup();

                if(ItemAdded!=null){
                    ItemAdded(this,new InventoryEventArgs(item));
                    print("item count " + mItems.Count);
                }
            }
          }
        }
        
    }
