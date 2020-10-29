using UnityEngine;

public class MyInventoryUI : MonoBehaviour
{
    public GameObject OwningPlayer;
    public Transform InventoryParent;
    public Transform ItemsParent;
    public InventorySlot[] Slots;
    
    private MyInventory Inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        if (OwningPlayer == null)
        {
            Debug.Log("The InventoryUI has no assigned player!");
            return;
        }
        
        Inventory = OwningPlayer.GetComponent<MyInventory>();
        Slots = ItemsParent.GetComponentsInChildren<InventorySlot>();

        if (Inventory == null)
        {
            Debug.Log("The Assigned player has no inventory!");
            return;
        }
        
        Inventory.OnInventoryChangedCallback += UpdateUI;
        Inventory.OnInventoryOpenedCallback += UpdateUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        if (Inventory.InventoryOpened)
        {
            InventoryParent.GetComponentInParent<Canvas>().enabled = true;
            
            Debug.Log("Updating Inventory UI!");

            for (int i = 0; i < Slots.Length; i++)
            {
                //If the 
                if (i < Inventory.InventoryItems.Count)
                {
                    //Call the AddItem function which updates the inventory ItemSlot UI
                    //with the item in the players inventory
                    Slots[i].AddItem(Inventory.InventoryItems[i]);
                }
                else
                {
                    Slots[i].ClearSlot();
                }
            }
        }
        else
        {
            InventoryParent.GetComponentInParent<Canvas>().enabled = false;
        }
    }
}
