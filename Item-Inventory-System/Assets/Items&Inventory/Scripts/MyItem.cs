using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/MyItem")]
public class MyItem : ScriptableObject
{
    //Id used to identify the item
    public bool IsDefaultItem = true;
    private int ID;
    public string Name;
    public string Descripton;
    public Sprite Icon = null;
    
    //The stats of the item
    //private Dictionary<string, int> stats;

    //public Transform ItemTransform;

    //Object model (for when this item can be dropped in the world)
    public Mesh Mesh;
    
    public MyItem(int id, string name, string descripton, Mesh mesh)
    {
        ID = id;
        Name = name;
        Descripton = descripton;
        Mesh = mesh;
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }
}
