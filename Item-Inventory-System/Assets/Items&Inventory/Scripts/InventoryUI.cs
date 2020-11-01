using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject OwningPlayer;
    public GameObject TheInventoryUI;
    public Transform ItemsParent;
    public InventorySlot[] Slots;
    
    private Inventory Inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        if (OwningPlayer == null)
        {
            Debug.Log("The InventoryUI has no assigned player!");
            return;
        }
        
        Inventory = OwningPlayer.GetComponent<Inventory>();
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
        if (Input.GetButtonDown("Inventory"))
        {
            TheInventoryUI.SetActive(!TheInventoryUI.activeSelf);
            Inventory.Opened();
        }
    }

    void UpdateUI()
    {
        if (Inventory.InventoryOpened)
        {
            Debug.Log("Updating Inventory UI!");

            for (int i = 0; i < Slots.Length; i++)
            {
                //If the index is less than the number of items in the inventory, there is an item in this slot
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
    }
}
