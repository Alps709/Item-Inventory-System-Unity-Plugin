using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float Radius = 5.0f;

    public bool IsFocused;
    public bool HasInteracted = false;

    public GameObject InteractingPlayer;

    public virtual void InteractedWith()
    {
        if (IsFocused)
        {
            float distance = Vector3.Distance(InteractingPlayer.transform.position, transform.position);
            if (distance <= Radius)
            {
                Interact();
                HasInteracted = true;
            }
        }
    }
    
    public virtual void Interact()
    {
        Debug.Log(InteractingPlayer.name + " interacted with Item!");
    }

    public void OnFocused(GameObject player)
    {
        IsFocused = true;
        InteractingPlayer = player;
        HasInteracted = false;
    }
    
    public void OnDefocused()
    {
        HasInteracted = false;
        IsFocused = false;
        InteractingPlayer = null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
