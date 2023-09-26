using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems; // максимальное кол-во предметов в стаке
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public int selectedSlot = -1;

    
    public bool AddItem(Item item)
    {
        // смотрим есть ли предмет не с максимальным кол-вом единиц стака
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        
        // ищем свободный слот
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public void DeleteItem()
    {
        if (selectedSlot >= 0 && selectedSlot < inventorySlots.Length)
        {
            InventorySlot slot = inventorySlots[selectedSlot];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                Destroy(itemInSlot.gameObject);
            }
        }
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
}
