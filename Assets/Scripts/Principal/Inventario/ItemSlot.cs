using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI _itemName;

    public void Set(InventoryItem item)
    {
        _itemName.text = item.data.itemName;
    }
}