using UnityEngine;
using System.Collections;

public class InventoryManager : Singleton<InventoryManager> {
    protected InventoryManager() { } // guarantee this will be always a singleton only - can't use the constructor!

    public bool DisplayInventory;
	// Use this for initialization
	void OnGui()
    {

    }
}
