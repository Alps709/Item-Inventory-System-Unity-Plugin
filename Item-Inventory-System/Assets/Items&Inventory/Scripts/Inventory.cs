using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> InventoryItems = new List<Item>();

    public int MaxSize = 3;
    private int Size = 0;

    private bool InventoryOpened = false;

    public delegate void OnInventoryChanged();
    public delegate void OnInventoryOpened();

    public OnInventoryChanged OnInventoryChangedCallback;
    public OnInventoryOpened OnInventoryOpenedCallback;
    public bool Add(Item item)
    {
        //If an item is a default item it is likely it has not been properly set up, so do not add it to the inventory
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

    public bool Remove(Item item)
    {
        Debug.Log("Item ID: " + item.ID);
        //If removal of item is possible
        if (InventoryItems.Remove(item))
        {
            //Reduce current inventory size by 1
            Size--;
            OnInventoryChangedCallback?.Invoke();
            
            //Drop the item in the world if it is droppable
            if (item.IsDroppable)
            {
                Transform dropLocation = gameObject.transform;
                dropLocation.localPosition += gameObject.transform.forward * 5.0f;
                item.Drop(dropLocation);
            }
            return true;
        }
        return false;
    }

    //This function is called from the InventoryUI script, to show/hide the inventory
    public void Opened()
    {
        InventoryOpened = !InventoryOpened;
        OnInventoryOpenedCallback?.Invoke();
    }

    public bool GetIsOpened()
    {
        return InventoryOpened;
    }

    //This sorting function doesn't work
    // public void SortByName()
    // {
    //     InventoryItems.OrderBy(item => item.name);
    // }
}
