using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInventory : MonoBehaviour
{
    public List<MyItem> InventoryItems = new List<MyItem>();

    private int MaxSize = 3;
    private int Size = 0;

    public delegate void OnInventoryChanged();

    public OnInventoryChanged OnInventoryChangedCallback;
    public bool Add(MyItem item)
    {
        if (!item.IsDefaultItem && Size < MaxSize)
        {
            Debug.Log("Adding the item: '" + item.name + "' to the inventory of '" + gameObject.name + "'");
            InventoryItems.Add(item);
            Size++;
            OnInventoryChangedCallback?.Invoke();
            return true;
        }
        
        //Failed to pickup
        Debug.Log("Can't pickup item! Inventory may be full or the item may be a default item!");
        return false;
    }

    public bool Remove(MyItem item)
    {
        if (InventoryItems.Remove(item))
        {
            Size--;
            OnInventoryChangedCallback?.Invoke();
            return true;
        }
        return false;
    }
}
