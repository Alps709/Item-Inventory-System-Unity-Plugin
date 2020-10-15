using System.Collections;
using System.Collections.Generic;
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
        
        if (HitInteractable != null)
        {
            SetFocus(HitInteractable);
        }
        else
        {
            RemoveFocus();
        }

        if (Input.GetMouseButtonDown(1) && FocusedItem != null)
        {
            //Pickup Item
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
            FocusedItem.OnFocused(transform);
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
}
