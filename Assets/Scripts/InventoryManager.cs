using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Name the user inputs in the inspector to see if the inventory contains it.
    [SerializeField] private string inventoryItemName;
    [SerializeField] private int inventoryItemID;

    // Array of names for the inventory items.
    private string[] inventoryNames = { "liam", "anish", "dylan", "prof. sengun", "marcus", "seb", 
        "avery", "rifle", "computer", "mouse", "money" };

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
        int inventorySize = inventoryNames.Length;

        // For each name in the inventoryNames array, create a new InventoryItem with a random ID and value.
        for (int i = 0; i < inventorySize; i++)
        {
            InventoryItem item = new InventoryItem(Random.Range(0, 73), inventoryNames[Random.Range(0, inventoryNames.Length - 1)], 
                Random.Range(0, 100));

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
                Debug.Log($"{target} found at index {i} in the inventory.");
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