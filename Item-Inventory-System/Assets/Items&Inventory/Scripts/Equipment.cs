using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot
{
    Head,
    Chest,
    Legs,
    Boots,
    Gloves,
    Weapon,
    Shield
}

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public int armourModifier;
    public int damageModifier;
    public List<EquipmentEffect> Effects;

    Equipment(List<EquipmentEffect> effects)
    {
        Effects = effects;
        
        if (Effects.Count != 0)
        {
            foreach (var effect in Effects)
            {
                //For some reason this is not being set properly, ends up being null in the effect class
                effect.SetOwningEquipment(this);
            
                Debug.Log("Set owning equipment for effects! Equipment name: " + this.name);
            }
        }
    }

    public override void Use(GameObject instigator)
    {
        base.Use(instigator);
        
        //Only cast effects if any are added
        if (Effects.Count != 0)
        {
            //Loop through and activate all effects
            foreach (var effect in Effects)
            {
                effect.CastEffect(instigator);
            }
        }
    }
}
