using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/MyItem")]
public class MyItem : ScriptableObject
{
    //Id used to identify the item
    public bool IsDefaultItem = true;
    public int ID = -1;
    public string Descripton;
    public Sprite Icon = null;
    
    //The stats of the item
    //private Dictionary<string, int> stats;

    //public Transform ItemTransform;

    //Object model (for when this item can be dropped in the world)
    public Mesh Mesh;
    
    public MyItem(int id, string descripton, Sprite icon, Mesh mesh)
    {
        ID = id;
        Descripton = descripton;
        Icon = icon;
        Mesh = mesh;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void Use()
    {
        //Use the item
        //What happens will depend on the type of derived item
        
        Debug.Log("Using " + name);
    }
}
