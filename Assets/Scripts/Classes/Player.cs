using System.Collections.Generic;
using UnityEngine;
public class Player : Attributes
{
    private List<InventoryItem> inventory;

    public List<InventoryItem> Inventory
    {
        get 
        {
            if (inventory == null)
            {
                inventory = new List<InventoryItem>();
            }
            return inventory;
        }
        set { inventory = value; }
    }
    
    public string[] Skills;
    public int Money;

    public void AddinventoryItem(InventoryItem item)
    {
        this.Strength += item.Strength;
        this.Defense += item.Defense;
        Inventory.Add(item);

    }
}
