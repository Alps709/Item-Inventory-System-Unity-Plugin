using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ForcePush", menuName = "Inventory/EquipmentEffects/ForcePush")]
public class ForcePush : EquipmentEffect
{
    public float ForceRadius = 5.0f;
    public float ConeAngle = 30.0f;
    public float Power = 50.0f;

    //Doesn't correctly set the owning equipment, I think it has something to do with object slicing
    public override void SetOwningEquipment(Equipment owningEquipment)
    {
        OwningEquipment = owningEquipment;
    }

    public override void CastEffect(GameObject PlayerInstigator)
    {
        base.CastEffect(PlayerInstigator);
        
        Debug.Log("Casting Force Push!");
        ForcePushCone(PlayerInstigator);
    }

    public void ForcePushCone(GameObject PlayerInstigator)
    {
        //Get the instigator's camera transform (NOTE: The camera for this plugin's player character is a child of the character gameobject, so it can have its own transform!)
        var instigatorCam = PlayerInstigator.GetComponentInChildren<Camera>();
        //Check if instigator camera is valid
        if (instigatorCam == null)
        {
            Debug.Log("Equipment effect instigator does not have a camera!"); 
            return;
        }
        
        Transform explosionPos = instigatorCam.transform;
        
        //Find all actors in a sphere around the instigator
        Collider[] overlappingActors = Physics.OverlapSphere(explosionPos.position, ForceRadius);

        foreach (var actor in overlappingActors)
        {
            //If the found actor is within the coneAngle of the instigator camera the apply the explosion force!
            if(actor.attachedRigidbody != null && Vector3.Angle(explosionPos.forward, actor.transform.position - explosionPos.transform.position) <= ConeAngle)
            {
                actor.attachedRigidbody.AddExplosionForce(Power, explosionPos.position, ForceRadius, 3.0f);
            }
        }
    }
}
