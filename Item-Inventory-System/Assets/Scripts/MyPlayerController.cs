using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    public Camera Cam;

    public Interactable FocusedItem;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
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
}
