using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    public string Name  { get; }
    public Sprite Image { get; }
    public void OnPickup();
}