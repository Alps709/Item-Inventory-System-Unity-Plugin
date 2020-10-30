using UnityEditor;
using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    public Camera Cam;
    public PlayerMovement PlayerMovement;
    public MouseLook CameraMovement;
    private bool MouseEnabled = false;
    public MyInventory Inventory;
    public Interactable FocusedItem;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get reference to player inventory
        Inventory = GetComponent<MyInventory>();
        if (Inventory == null)
        {
            Debug.LogError("Player couldn't find inventory reference!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Find if there is an item to interact with
        //and if there is one you're trying to interact with, interact with it
        FindInteractable();
        
        //Do everything you need to do with UI
        OpenUI();
    }

    void SetFocus(Interactable NewFocus)
    {
        if (NewFocus != FocusedItem)
        {
            //Defocus previous item
            if (FocusedItem != null)
            {
                FocusedItem.OnDefocused();
            }
            
            //Set new focused item
            FocusedItem = NewFocus;
            FocusedItem.OnFocused(gameObject);
        }
    }

    void RemoveFocus()
    {
        if (FocusedItem != null)
        {
            FocusedItem.OnDefocused();
        }
        FocusedItem = null;
    }

    void Interact()
    {
        FocusedItem.InteractedWith();
    }

    void FindInteractable()
    {
        Ray HitRay = Cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit RayCastHit;
        Interactable HitInteractable = null;
        
        if (Physics.Raycast(HitRay, out RayCastHit, 100))
        {
            HitInteractable = RayCastHit.collider.GetComponent<Interactable>();
        }
        
        //Only null if not an interactable
        if (HitInteractable != null)
        {
            SetFocus(HitInteractable);

            //Check if left mouse button has been clicked, to interact with the item
            if (Input.GetMouseButtonDown(1))
            {
                Interact();
            }
        }
        else
        {
            RemoveFocus();
        }
        
        if (Input.GetKeyDown(KeyCode.E) && FocusedItem)
        {
            Interact();
        }
    }
    
    //Handle what the player controller should do when certain UI is opened
    void OpenUI()
    {
        //Handle what happens when Inventory is opened/closed
        if (Input.GetButtonDown("Inventory"))
        {
            //Disable/enable mouse
            MouseEnabled = !MouseEnabled;
       
            //If inventory open
            if (MouseEnabled)
            {
                //Enable cursor and stop movement
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                PlayerMovement.MovementEnabled = false;
                CameraMovement.MovementEnabled = false;
            }
            else
            {
                //Disable cursor and enable movement
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                PlayerMovement.MovementEnabled = true;
                CameraMovement.MovementEnabled = true;
            }
        }
    }
}
