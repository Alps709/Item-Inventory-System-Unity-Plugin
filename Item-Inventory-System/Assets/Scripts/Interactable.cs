using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float Radius = 5.0f;

    public bool IsFocused;
    public bool HasInteracted = false;

    public Transform Player;

    void Update()
    {
        if (IsFocused && !HasInteracted)
        {
            float distance = Vector3.Distance(Player.position, transform.position);
            if (distance <= Radius)
            {
                Interact();
                HasInteracted = true;
            }
        }
    }
    
    public virtual void Interact()
    {
        Debug.Log("Interacted with Item!");
    }

    public void OnFocused(Transform PlayerTransform)
    {
        IsFocused = true;
        Player = PlayerTransform;
        HasInteracted = false;
    }
    
    public void OnDefocused()
    {
        HasInteracted = false;
        IsFocused = false;
        Player = null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
