using System.Collections.Generic;
using UnityEngine;

public class LiamInventoryManager : MonoBehaviour
{
    // Name the user inputs in the inspector to see if the inventory contains it.
    [SerializeField] private string inventoryItemName;
    [SerializeField] private int inventoryItemID;
    [SerializeField] private int inventoryItemValue;

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
        // TO DO: If Q key is pressed, perform a quick sort 
        else if (Input.GetKeyDown(KeyCode.Q)) 
        {
            //partition(inventoryList, 0, inventoryList.Count - 1);
            QuickSortByValue(inventoryList, 0, inventoryList.Count - 1);
        }

        LiamQuizDebugs();
    }

    // Populate the inventory with random entries.
    private void InitializeInventory()
    {
        int inventorySize = inventoryNames.Length;

        // For each name in the inventoryNames array, create a new InventoryItem with a random ID and value.
        for (int i = 0; i < inventorySize; i++)
        {
            InventoryItem item = new InventoryItem(Random.Range(0, 73), inventoryNames[i], Random.Range(0, 100));

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
                Debug.Log($"{target} found in the inventory with an ID of {list[i].ID} and a value of {list[i].value}.");
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

    // TO DO: Partition used for Quick Sort
    public int partition(List<InventoryItem> list, int first, int last)
    {
        InventoryItem pivotItem = list[last];
        int smaller = first - 1;

        for (int element = first; element < last; element++)
        {
            if (list[element].value < pivotItem.value)
            {
                element++;

                InventoryItem temporary = list[smaller + 1];
                list[smaller] = list[element];
                list[element] = temporary;
            }
        }

        InventoryItem temporaryNext = list[smaller + 1];
        list[smaller + 1] = list[last];
        list[last] = temporaryNext;

        return smaller + 1;
    }

    // TO DO: Quick Sort Method
    public void QuickSortByValue(List<InventoryItem> list, int first, int last)
    {
        if (list[first].value < list[last].value)
        {
            int pivot = partition(list, first, last);

            QuickSortByValue(list, first, pivot - 1);
            QuickSortByValue(list, pivot + 1, last);

        }

        for (int i = 0; i < list.Count - 1; i++) 
        {
            Debug.Log("ID: " + list[i].ID + "\nName: " + list[i].Name + "\nValue: " + list[i].value);
        }
    }



    /// <summary>
    /// Liam's Quiz 8 work that contains the functions for questions 1, 2, and 3.
    /// Additionally, also contains a function to debug the functions that are asked for in the quiz questions to ensure they work.
    /// </summary>

    // Adds an item to the inventory list with the given ID, name, and value.
    public void AddItemToInventory(int id, string name, int value)
    {
        // Creates the new item.
        InventoryItem newItem = new InventoryItem(id, name, value);

        // Adds the new item to the inventory list.
        inventoryList.Add(newItem);
    }

    // Gives the total value of all items in the inventory.
    public int CalculateTotalInventoryValue()
    {
        // Variable to store the total value of the inventory.
        // This can be made into a non-local variable if you want to access it from outside this function without calling this function.
        int totalValue = 0;

        // Loops through each item in the inventory list and adds the value of each item to the total value.
        for (int i = 0; i < inventoryList.Count; i++)
        {
            // Adds the current item's value to the total value.
            totalValue += inventoryList[i].value;
        }

        // Returns the total value of the inventory.
        return totalValue;
    }

    // Given a min and max value, returns a list of items in the inventory that have a value within that range.
    public List<InventoryItem> FilterItemsByValueRange(int minValue, int maxValue)
    {
        // List to store the filtered items.
        List<InventoryItem> filteredItems = new List<InventoryItem>();

        // Loops through each item in the inventory list and checks its value.
        for (int i = 0; i < inventoryList.Count; i++)
        {
            // If the value of the current item is within the min and max range, add it to the filtered items list.
            if (inventoryList[i].value >= minValue && inventoryList[i].value <= maxValue)
            {
                filteredItems.Add(inventoryList[i]);
            }
        }

        // Once the loop is done, return the list of filtered items.
        return filteredItems;
    }

    private void LiamQuizDebugs()
    {
        // If I, run the AddItemToInventory function with the given values.
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddItemToInventory(Random.Range(0, 58), inventoryItemName.ToLower(), Random.Range(0, 100));
        }
        // If C, debug the total value of the inventory.
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Total value of inventory: " + CalculateTotalInventoryValue());
        }
        // If F, filter the items in the inventory by a value range and debug the results.
        else if (Input.GetKeyDown(KeyCode.F))
        {
            List<InventoryItem> filteredItems = FilterItemsByValueRange(8, 54);

            for (int i = 0; i < filteredItems.Count; i++)
            {
                Debug.Log($"ID: {filteredItems[i].ID}, Name: {filteredItems[i].Name}, Value: {filteredItems[i].value}");
            }
        }
    }
}

/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //public int inventoryNames;
    private List <InventoryItem>inventoryList = new List<InventoryItem>();
    InventoryItem[] iLArray;

    private void Start()
    {
        //Make random number of values and ID's
        //
        string [] arr = { "Liam", "Anish", "Dylan" };
        for (int i = 0; i < 5; i++) 
        {
            inventoryList.Add(new InventoryItem(i, arr[Random.Range(0, arr.Length - 1)], Random.Range(0, 37)));
        }

        iLArray = inventoryList.ToArray();
        Debug.Log("UNSORTED ARRAY: ");
        DisplayInDebug(iLArray);

        QuickSortByValue(iLArray, 1, iLArray.Length - 1);
        Debug.Log("QUICK SORTED ARRAY: ");
        DisplayInDebug(iLArray);

    }

    [SerializeField] private string inventoryName;

// _________________________________________________________________________________________________________
// TEST METHODS

    public int partition(InventoryItem[] array, int first, int last)
    {
        int pivot = last;
        int smaller = first - 1;

        for (int element = first; element < last; element++)
        {
            if (element < pivot)
            {
                element++;

                int temporary = smaller;
                array[smaller] = array[element];
                element = temporary;
            }
        }

        int temporaryNext = smaller + 1;
        array[smaller + 1] = array[last];
        last = temporaryNext;

        return smaller + 1;

    }



    public void QuickSortByValue(InventoryItem[] array, int first, int last)
    {
        if (array[first].value < array[last].value)
        {
            int pivot = partition(array, first, last);

            QuickSortByValue(array, first, pivot - 1);
            QuickSortByValue(array, pivot + 1, last);

        }
    }

    void DisplayInDebug(InventoryItem[] displayedArray) 
    {
        //Debug.Log(displayedArray.Length);
        for (int i = 0; i < displayedArray.Length; i++) 
        {
            //Debug.Log("LOOP " + i);
            Debug.Log("ID: " + displayedArray[i].ID + "\nName: " + displayedArray[i].Name + "\nValue: " + displayedArray[i].value);
        }
    }

    void Update()
    {
        
    }

}

// _________________________________________________________________________________________________________
// EXAMPLE METHODS
  public int partition(InventoryItem[] array, int first, int last)
    {
        int pivot = last;
        int smaller = (first - 1);

        for (int element = first; element < last; element++)
        {
            if (element < pivot)
            {
                element++;

                int temporary = smaller;
                array[smaller] = array[element];
                element = temporary;
            }
        }

        int temporaryNext = smaller + 1;
        array[smaller + 1] = array[last];
        last = temporaryNext;

        return smaller + 1;
    }

    public void QuickSortByValue(InventoryItem[] array, int first, int last)
    {
        if (first < last)
        {
            int pivot = partition(array, first, last);

            QuickSortByValue(array, first, pivot - 1);
            QuickSortByValue(array, pivot + 1, last);

        }
    }
  
  public int partition(int []array, int first, int last)
  {
    int pivot = array[last];
    int smaller = (first - 1);

    for (int element = first; element < last; element++)
    {
      if (array[element] < pivot)
      {
        element++;

        int temporary = array[smaller];
        array[smaller] = array[element];
        array[element] = temporary;
      }
    }

    int temporaryNext = array[smaller + 1];
    array[smaller + 1] = array[last];
    array[last] = temporaryNext;

    return smaller + 1;

  }

  public void quickSort(int []array, int first, int last)
  {
    if (first < last)
    {
      int pivot = partition(array, first, last);

      quickSort(array, first, pivot - 1);
      quickSort(array, pivot + 1, last);

    }
  }
*/