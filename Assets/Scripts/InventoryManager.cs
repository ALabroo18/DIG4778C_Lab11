

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InventoryManager : MonoBehaviour
{
    private List <InventoryItem>inventoryList = new List<InventoryItem>();
    public int ID;
    private void Start()
    {
        //Make random number of values and ID's
        //
        string [] arr = { "Liam", "Anish", "Dylan" };
        inventoryList.Add(new InventoryItem(10, arr[Random.Range(0, arr.Length - 1)], Random.Range(0, 37)));
        inventoryList.Add(new InventoryItem(10, arr[Random.Range(0, arr.Length - 1)], Random.Range(0, 37)));
        inventoryList.Add(new InventoryItem(10, arr[Random.Range(0, arr.Length - 1)], Random.Range(0, 37)));


    }
    [SerializeField] private string inventoryName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) {
            int result = BinarySearch(inventoryList, ID);
            if (result > -1 )
            {
                Debug.Log($"Target value {ID} found at {inventoryList[result].Name}");
            }
            else
            {
                Debug.Log("didnt find anything.");
            }


        }
    }
    // Binary Search Method
    private static int BinarySearch(List<InventoryItem> list, int target)
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
