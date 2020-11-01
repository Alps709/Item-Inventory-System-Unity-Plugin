using UnityEngine;


public class EquipmentEffect : ScriptableObject
{
    //The owning equipment is set on the effect by the owning equipment start/constructor
    protected Equipment OwningEquipment;

    //Doesn't correctly set the owning equipment, I think it has something to do with object slicing
    public virtual void SetOwningEquipment(Equipment owningEquipment)
    {
        OwningEquipment = owningEquipment;
    }
    public virtual void CastEffect(GameObject PlayerInstigator)
    {
        Debug.Log("Casting Equipment Effect!");
    }
}
