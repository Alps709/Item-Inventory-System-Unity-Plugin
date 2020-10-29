using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItemPickup : Interactable
{
    public MyItem Item;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    public void PickUp()
    {
        //Get the inventory of the interacting player (may be null)
        var inventory = InteractingPlayer.GetComponent<MyInventory>();
        if (inventory == null)
        {
            Debug.Log("The player trying to pick up the item: " + Item.name + ", does not have an inventory!");
            return;
        }
        
        //If we can add the item to the players inventory, also delete it from the world
        if (inventory.Add(Item))
        {
            Destroy(gameObject);
        }
    }
}
