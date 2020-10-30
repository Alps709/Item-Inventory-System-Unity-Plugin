using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public MyItem Item;

    public Image Icon;
    public Button RemoveButton;
    private MyInventory PlayerInventory;


    private void Start()
    {
        //Get reference to player inventory by finding the InventoryUI and getting owning player
        PlayerInventory = GetComponentInParent<MyInventoryUI>().OwningPlayer.GetComponent<MyInventory>();

        if (PlayerInventory == null)
        {
            Debug.Log("Reference to player inventory couldn't be found!");
        }
    }

    public void AddItem(MyItem newItem)
    {
        Item = newItem;

        Icon.sprite = Item.Icon;
        Icon.enabled = true;
        RemoveButton.interactable = true;
        RemoveButton.enabled = true;
        RemoveButton.gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        Item = null;

        Icon.sprite = null;
        Icon.enabled = false;
        RemoveButton.gameObject.SetActive(false);
    }

    public void OnRemoveButton()
    {
        PlayerInventory.Remove(Item);
    }

    public void UseItem()
    {
        if (Item != null)
        {
            Item.Use();
        }
    }
}
