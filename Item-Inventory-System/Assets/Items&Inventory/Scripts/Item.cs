using System.Collections;
using UnityEngine;

//IMPORTANT: These types must be the exact spelling of the derived classes you make,
//so it can get the correct type by converting the value to a string
//eg. a derived item class called "class Consumable" must be "Consumable" for it's type
//This is used for the custom editor window item asset creation in "ItemCreatorEditorWindow.cs"
public enum ItemType
{
    Item,
    Equipment
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/MyItem")]
public class Item : ScriptableObject
{
    
    public bool IsDefaultItem = true;

    public bool IsDroppable = true;
    
    //Id used to identify the item (suppose to be unique)
    public int ID = -1;
    
    public string Descripton;
    
    //The Sprite used as the Item's icon in the inventory
    public Sprite icon = null;

    //Object model (for when this item can be dropped in the world)
    public Mesh mesh;

    public Material material;

    // Start is called before the first frame update
    void Start()
    {
    }

    public virtual void Use(GameObject instigator)
    {
        //Use the item
        //What happens will depend on the type of derived item
        
    }
    
    public virtual void Drop(Transform dropLocation)
    {
        //Drop the item
        //What happens will depend on the type of derived item
        //But default functionality will be to create the item in the world
        if (IsDroppable)
        {
            Debug.Log("Dropping " + name);
            
            GameObject item = new GameObject(this.name);
            
            //Set transform
            item.transform.SetPositionAndRotation(dropLocation.position, Quaternion.identity);
            Debug.Log("Dropping " + name + " at: " + dropLocation.position);

            //Add the item pickup script and set the item
            var itemScript = item.AddComponent<MyItemPickup>();
            itemScript.Item = this;
            
            //Add the mesh, material, collider, and rigidbody
            var mesh = item.AddComponent<MeshFilter>();
            mesh.mesh = this.mesh;
            
            var meshRenderer = item.AddComponent<MeshRenderer>();
            meshRenderer.material = material;
            
            var meshCollider = item.AddComponent<MeshCollider>();
            meshCollider.convex = true;
            
            item.AddComponent<Rigidbody>();
        }
        else
        {
            Debug.Log("Cannot drop " + name);
        }
        
    }
}
