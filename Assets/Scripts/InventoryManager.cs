using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int inventoryNames;
    private List <InventoryItem>inventoryList = new List<InventoryItem>();

    private void Start()
    {
        //Make random number of values and ID's
        //
        string [] arr = { "Liam", "Anish", "Dylan" };
        inventoryList.Add(new InventoryItem(10, arr[Random.Range(0, arr.Length - 1)], Random.Range(0, 37)));

    }
    [SerializeField] private string inventoryName;
}
