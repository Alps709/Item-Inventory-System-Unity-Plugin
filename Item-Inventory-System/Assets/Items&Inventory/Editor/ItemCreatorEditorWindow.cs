using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemCreatorEditorWindow : EditorWindow
{
    private string itemName = "Default Name";
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
        
        icon = (Sprite) EditorGUILayout.ObjectField("Inventory Icon", icon, typeof(Sprite));
        
        mesh = (Mesh) EditorGUILayout.ObjectField("Item Mesh", mesh, typeof(Mesh));
        
        material = (Material) EditorGUILayout.ObjectField("Item Material", material, typeof(Material));
        
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
                
                // Effects = (EquipmentEffect) EditorGUILayout.ObjectField("Equipment Effect", Effects[i], typeof(EquipmentEffect[]));
                // //Add array to be filled with EquipmentEffects here
                // for (int i = 0; i < Effects.Length; i++)
                // {
                //     Effects[i] = (EquipmentEffect) EditorGUILayout.ObjectField("Equipment Effect", Effects[i], typeof(EquipmentEffect[]));
                // }
                break;
            }
        }
        
        CreatePrefab = (bool) EditorGUILayout.Toggle("Create Prefab", CreatePrefab);
        if (CreatePrefab)
        {
            if (itemType == ItemType.Equipment)
            {
                EditorGUILayout.LabelField("Equipment is always droppable!");
                IsDropable = true;
            }
            
            IsDropable = (bool) EditorGUILayout.Toggle("Is Droppable", IsDropable);
        }
        
        if (GUILayout.Button("Create Item"))
        {
            CreateScriptableItemInstance(itemType);
            Debug.Log("Item created!");
        }
    }

    private void CreateScriptableItemInstance(ItemType itemType)
    {
        //Add the temp classes that the item could be, here
        Item tempItem = null;
        Equipment tempEquipment = null;
        string path = "Assets/Items&Inventory/Items&Equipment/";
        
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
                break;
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
                break;
            }
        }
        
        
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
