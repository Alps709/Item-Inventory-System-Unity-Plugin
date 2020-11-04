using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemCreatorEditorWindow : EditorWindow
{
    //The file path to save the objects
    string path = "Assets/Items&Inventory/Items&Equipment/";
    
    private string itemName = "Default Item Name";
    public string description = "Default description";
    public Sprite icon;
    public Mesh mesh;
    public Material material;
    
    public bool CreatePrefab = false;
    public bool IsDropable = false;
    public ItemType itemType = ItemType.Item;

    public List<EquipmentEffect> Effects;
    
    [MenuItem("Tools/ItemCreatorWindow")]
    public static void ShowWindow()
    {
        GetWindow<ItemCreatorEditorWindow>();
    }
    
    //Window Code
    public void OnGUI()
    {
        //Window title
        GUILayout.Label("Item Creator Window", EditorStyles.boldLabel);
        
        //Item Name Field
        itemName = EditorGUILayout.TextField("Item Name",itemName);
        description = EditorGUILayout.TextField("Item Description",description);
        
        icon = (Sprite) EditorGUILayout.ObjectField("Inventory Icon", icon, typeof(Sprite), false);
        
        mesh = (Mesh) EditorGUILayout.ObjectField("Item Mesh", mesh, typeof(Mesh), false);
        
        material = (Material) EditorGUILayout.ObjectField("Item Material", material, typeof(Material), false);
        
        itemType = (ItemType) EditorGUILayout.EnumPopup("Item Type", itemType);
        
        switch (itemType)
        {
            case ItemType.Equipment:
            {
                ScriptableObject target = this;
                SerializedObject so = new SerializedObject(target);
                SerializedProperty stringsProperty = so.FindProperty("Effects");
                
                EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
                so.ApplyModifiedProperties();
                break;
            }
        }
        
        CreatePrefab = EditorGUILayout.Toggle("Create Prefab", CreatePrefab);
        if (CreatePrefab)
        {
            if (itemType == ItemType.Equipment)
            {
                EditorGUILayout.LabelField("Equipment is always droppable!");
                IsDropable = EditorGUILayout.Toggle("Is Droppable", IsDropable);
                IsDropable = true;
            }
            else
            {
                IsDropable = EditorGUILayout.Toggle("Is Droppable", IsDropable);
            }
        }
        
        if (GUILayout.Button("Create Item"))
        {
            CreateItem(itemType, CreatePrefab);

            Debug.Log("Item created!");
        }
    }

    private bool CreateItem(ItemType itemType, bool CreatePrefab)
    {
        //Add all the temp classes that the item could be, here
        //And then extend the switch
        Item tempItem = null;
        Equipment tempEquipment = null;

        switch (itemType.ToString())
        {
            //These must be the exact names of the enum!
            case "Item":
            {
                tempItem = CreateInstance<Item>();
                tempItem.IsDefaultItem = false;
                tempItem.IsDroppable = IsDropable;
                tempItem.name = itemName;
                tempItem.Descripton = description;
                tempItem.icon = icon;
                tempItem.material = material;
                tempItem.mesh = mesh;
                path += tempItem.name + ".asset";
                AssetDatabase.CreateAsset(tempItem, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
                if (CreatePrefab)
                {
                    CreateItemPrefab(tempItem);
                }
                return true;
            }
            
            //These must be the exact names of the enum!
            case "Equipment":
            {
                tempEquipment = CreateInstance<Equipment>();
                tempEquipment.IsDefaultItem = false;
                tempEquipment.IsDroppable = IsDropable;
                tempEquipment.name = itemName;
                tempEquipment.Descripton = description;
                tempEquipment.icon = icon;
                tempEquipment.material = material;
                tempEquipment.mesh = mesh;
                tempEquipment.Effects = Effects;
                path += tempEquipment.name + ".asset";
                AssetDatabase.CreateAsset(tempEquipment, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
                if (CreatePrefab)
                {
                    CreateItemPrefab(tempEquipment);
                }
                return true;
            }
        }

        return false;
    }

    private void CreateItemPrefab(Item item)
    {
        GameObject prefabItem = new GameObject(item.name);
        
        //Add the item pickup script and set the item
        var itemScript = prefabItem.AddComponent<MyItemPickup>();
        itemScript.Item = item;
            
        //Add the mesh, material, collider, and rigidbody
        var mesh = prefabItem.AddComponent<MeshFilter>();
        mesh.mesh = this.mesh;
            
        var meshRenderer = prefabItem.AddComponent<MeshRenderer>();
        meshRenderer.material = material;
            
        var meshCollider = prefabItem.AddComponent<MeshCollider>();
        meshCollider.convex = true;
            
        prefabItem.AddComponent<Rigidbody>();

        string path = "Assets/Items&Inventory/Items&Equipment/ItemPrefabs/" + prefabItem.name + ".prefab";
        
        Debug.Log("Prefab creation path: " + prefabItem.name);
        
        PrefabUtility.SaveAsPrefabAsset(prefabItem, path);
        
        Destroy(prefabItem);
    }

    // private void CreateAsset(Type type)
    // {
    //     var fileName = type.ToString().Split('.').Last();
    //     var asset = ScriptableObject.CreateInstance(type);
    //     var path = AssetDatabase.GetAssetPath(Selection.activeObject);
    //
    //     path = path.IsNullOrEmpty()
    //         ? "Assets/Resources"
    //         : Path.GetDirectoryName(path);
    //
    //     path = AssetDatabase.GenerateUniqueAssetPath(string.Format("{0}/{1}.asset", path, fileName));
    //     AssetDatabase.CreateAsset(asset, path);
    //     AssetDatabase.SaveAssets();
    //     AssetDatabase.Refresh();
    //     EditorUtility.FocusProjectWindow();
    //     Selection.activeObject = asset;
    //     fieldInfo.SetValue(Property.serializedObject.targetObject, asset);
    // }
    
}
