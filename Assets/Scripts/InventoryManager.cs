using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Name the user inputs in the inspector to see if the inventory contains it.
    [SerializeField] private string inventoryItemName;
    [SerializeField] private int inventoryItemID;

    // List of names for the inventory items.
    private List<string> inventoryNames = new List<string> { "liam", "anish", "dylan", "prof. sengun", "marcus", "seb", 
        "avery", "rifle", "computer", "mouse", "money", "monkey", "elephant", "monitor" };

    // List of InventoryItems that will be used to store the inventory.
    private List <InventoryItem>inventoryList = new List<InventoryItem>();

    private void Start()
    {
        // Populate the inventory with entries when the game starts.
        InitializeInventory();
    }

    private void Update()
    {
        // If L key is pressed, run the linear search of the inventory.
        if (Input.GetKeyDown(KeyCode.L))
        {
            // When the user inputs a name, the program will search for it in the inventory list.
            LinearSearchByName(inventoryList, inventoryItemName.ToLower());
        }
        // If B key is pressed, run the binary search of the inventory.
        else if (Input.GetKeyDown(KeyCode.B))
        {
            int result = BinarySearchByID(inventoryList, inventoryItemID);
            if (result > -1)
            {
                Debug.Log($"Target value {inventoryItemID} found at {inventoryList[result].Name}");
            }
            else
            {
                Debug.Log("didnt find anything.");
            }
        }
    }

    // Populate the inventory with random entries.
    private void InitializeInventory()
    {
        int inventorySize = inventoryNames.Count;

        // For each name in the inventoryNames array, create a new InventoryItem with a random ID and value.
        for (int i = 0; i < inventorySize; i++)
        {
            // Get a random name from the inventoryNames list.
            string itemName = inventoryNames[Random.Range(0, inventoryNames.Count - 1)];
            int itemID = Random.Range(0, inventorySize);
            int itemValue = Random.Range(0, 100);

            // Create a new inventory item with a random ID and value that uses the above random name.
            InventoryItem item = new InventoryItem(itemID, itemName, itemValue);

            for (int j = 0; j < inventoryList.Count; j++)
            {
                bool duplicate = false;

                if ((inventoryList[j].Name == itemName) || (inventoryList[j].ID.ToString() == itemID.ToString()) ||
                    (inventoryList[j].value.ToString() == itemValue.ToString()))
                {
                    duplicate = true;
                }
                
                while (duplicate)
                {
                    Debug.Log("Duplicate found, removing from inventoryNames list.");
                    inventoryNames.Remove(itemName);
                    break;
                }
                duplicate = false;
            }

            // Remove the name from the inventoryNames list so it cannot be used again.
            inventoryNames.Remove(itemName);

            // Add the new item to the inventory list.
            inventoryList.Add(item);
        }
    }

    // Search the inventory for the target name using linear search.
    private void LinearSearchByName(List<InventoryItem> list, string target)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Name == target)
            {
                // If the target is found, debug where it was found and exit the funciton.
                Debug.Log($"{target} found at index {i} in the inventory with ID of {list[i].ID} and value of {list[i].value}.");
                return;
            }
        }

        // The target was not found, so debug saying it was not found.
        Debug.Log($"{target} not found in the inventory.");
    }

    // Binary Search Method
    private static int BinarySearchByID(List<InventoryItem> list, int target)
    {
        int left = 0;
        int right = list.Count - 1;
        Debug.Log(list[target].ID + list[target].Name);

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (list[mid].ID == target)
            {
                return mid; // Return the index if the target is found
            }
            else if (list[mid].ID < target)
            {
                left = mid + 1; // Narrow the search to the upper half
            }
            else
            {
                right = mid - 1; // Narrow the search to the lower half
            }
        }

        return -1; // Return -1 if the target is not found
    }
}