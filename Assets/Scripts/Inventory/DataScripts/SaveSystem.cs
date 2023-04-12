using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// IN THIS SCRIPT: Use this to save the items and quantities in your Inventory for your next session
// WARNING: This script uses UNITY Editor to simplify the process of setting it up
// USE THIS SCRIPT by attaching it in the same GameObject as the Inventory Script
public class SaveSystem : MonoBehaviour
{

    // Place all items in the game by their ID order in this list(ID starts at 0)
    public List<Item> itemLibrary = new List<Item>();

    // The string that is use to store the current Invetory Data when SaveInventory is called
   string inventoryString = "";


    

    // Runs everytime the SaveInventory() is called
    public void TransformDataToString()
    {
        // For each item the script saves the ID and quantity of it
        foreach(Item item in Inventory.instance.itemList)
        {
            inventoryString = inventoryString + item.ID + ":" + Inventory.instance.quantityList[Inventory.instance.itemList.IndexOf(item)] + "/";
        }


    }


    // Call this function everytime you want to save the current Inventory
    public void SaveInventory()
    {
        TransformDataToString();
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        InventoryData data = new InventoryData(inventoryString);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    // Call this function everytime you want to Load the saved Inventory data
    public void LoadInventory()
    {
        inventoryString = "";
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
       InventoryData data = (InventoryData)bf.Deserialize(file);
        file.Close();


      
      ReadInventoryData(data.inventoryString);

        // Update UI after Load
        Inventory.instance.UpdateInventoryUI();
      
    }

    // ReadInventoryData transforms the single saved string back to items and it's called at the end of LoadInventory()
    public void ReadInventoryData(string data)
    {
       
        Inventory.instance.itemList.Clear();
        Inventory.instance.quantityList.Clear();

        string[] splitData = data.Split(char.Parse("/"));

       
            foreach (string stg in splitData)
            {
            
                string[] splitID = stg.Split(char.Parse(":"));

            if (splitID.Length >= 2)
            {
                Inventory.instance.itemList.Add(itemLibrary[int.Parse(splitID[0])]);
                Inventory.instance.quantityList.Add(int.Parse(splitID[1]));
            }


            }
        
    }

}
