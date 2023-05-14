using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new InventoryModel", menuName = "InventorySO")]
public class InventoryModel : ScriptableObject, IInventoryModel
{
    private List<Item> inventoryItemList;
    [SerializeField] private int size = 25;
    public event Action<Dictionary<int, Item>> OnModelChange;

    public void Initialize(List<Item> itemToAddList)
    {
        // Add null to all slot in inventory base on the size of it
        inventoryItemList = new List<Item>();
        for (int i = 0; i < size; i++) {
            inventoryItemList.Add(null);    
        }

        // Add item in the inventory from the given intial list
        for (int i = 0; i < itemToAddList.Count; i++) {
            AddItem(itemToAddList[i]);
        }

        // Notify UI
        OnModelChange?.Invoke(GetCurrentState());
    }
    public List<Item> GetItemList()
    {
        return inventoryItemList;
    }
    public int GetSize() { return size; }
    public void AddItem(Item itemToAdd)
    {
        // Add item base on whether it is stackable or not
        // itemToAdd is a deep copy and allow to be alter
        if (itemToAdd.IsStackable())
        {
            AddStackableItem(itemToAdd);
        }
        else
        {
            AddNonStackableItem(itemToAdd);
        }
        OnModelChange?.Invoke(GetCurrentState());
    }
    private void AddStackableItem(Item itemToAdd)
    {
        // Add stackable item by finding a slot that has the same and increase the count
        // If there is extra, repeat until no more item to add or no same items
        int quantity = itemToAdd.GetCount();
        int i = 0;
        while (itemToAdd.GetCount() > 0 && i < inventoryItemList.Count)
        {
            inventoryItemList[i]?.AddSameItem(itemToAdd);
            i++;
        }
        i = 0;
        while (itemToAdd.GetCount() > 0 && i < inventoryItemList.Count)
        {
            if (inventoryItemList[i] == null)
            {
                inventoryItemList[i] = new Item(itemToAdd.GetItemClass(), 0, itemToAdd.GetCondition());
                inventoryItemList[i].AddSameItem(itemToAdd);
            }
            i++;
        }
    }
    private void AddNonStackableItem(Item itemToAdd)
    {
        // Add nonstackable item by adding one to an empty slot 
        // If there is extra, repeat until no more item to add
        int i = 0;
        while (itemToAdd.GetCount() > 0  && i < inventoryItemList.Count)
        {  
            if (inventoryItemList[i] == null)
            {
                inventoryItemList[i] = new Item(itemToAdd.GetItemClass(), condition: itemToAdd.GetCondition());
                itemToAdd.SubCount(1);
            }
            i++;
        }
    }
    public bool HandleSwapItem(Item item, int index)
    {
        // Only handle swap right now
        bool result = this.SetData(index, item);
        OnModelChange?.Invoke(GetCurrentState());
        return result;
    }
    public bool HandleAddItem(Item item, int index)
    {
        // Only handle swap right now
        inventoryItemList[index].AddSameItem(item);
        OnModelChange?.Invoke(GetCurrentState());
        return true;
    }
    public Dictionary<int, Item> GetCurrentState() {
        Dictionary<int, Item> returnValue = new Dictionary<int, Item>();

        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            if (inventoryItemList[i] == null) continue;
            returnValue[i] = inventoryItemList[i];
        }

        return returnValue;
    }
    public Item GetData(int index) {
        if (inventoryItemList[index] == null) return null;
        return new Item(inventoryItemList[index]); 
    }
    public bool SetData(int index, Item item)
    {
        inventoryItemList[index] = item;
        return true;
    }
}
