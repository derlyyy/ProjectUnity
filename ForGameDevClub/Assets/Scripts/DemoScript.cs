using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public static DemoScript instance;
    
    public InventoryManager inventoryManager;
    public Item[] items;

    private void Awake()
    {
        instance = this;
    }

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(items[id]);
    }

    public void UseItem()
    {
        
    }
}
