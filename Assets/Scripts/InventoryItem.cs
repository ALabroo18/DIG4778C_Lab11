using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public int ID;
    public string Name;
    public int value;

    public InventoryItem(int ID, string Name, int value)
    {
        this.ID = ID;
        this.Name = Name;
        this.value = value;
    }

    private void Awake()
    {

    }
}