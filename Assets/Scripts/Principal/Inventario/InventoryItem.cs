using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public InventoryItemData data;
    public int stackSize;
    public InventoryItem(InventoryItemData itemData)
    {
        data = itemData;
    }

    public void AddStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
}
