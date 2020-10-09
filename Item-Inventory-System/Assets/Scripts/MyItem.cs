using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    //Id used to identify the item
    private uint ID;
    private new string name;
    private string descripton;

    //Object model (for when this item can be dropped in the world)
    protected Mesh mesh;

    //The stats of the item
    private Dictionary<string, int> stats;

    public Item(uint id, string name, string descripton, Component[] components, Dictionary<string, int> stats)
    {
        ID = id;
        this.name = name;
        this.descripton = descripton;
        this.stats = stats;
    }
}
