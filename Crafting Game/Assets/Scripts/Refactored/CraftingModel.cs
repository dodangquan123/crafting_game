using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new CraftingModel", menuName = "CraftingSO")]
public class CraftingModel : ScriptableObject, IInventoryModel
{
    private Dictionary<int, Item> itemList = new Dictionary<int, Item>();
    public event Action<Dictionary<int, Item>> OnModelChange;
    public Item GetData(int index)
    {
        if (itemList[index] == null) return null;
        return new Item(itemList[index]);
    }
    public bool SetData(int index, Item item)
    {
        itemList[index] = item;
        return true;
    }

    public bool HandleAddItem(Item item, int index)
    {
        if (index >= 0 && index < 3)
        {
            itemList[index].AddSameItem(item);
            OnModelChange?.Invoke(GetCurrentState());
            return true;
        }

        return false;
    }

    public bool HandleSwapItem(Item item, int index)
    {
        if (index >= 0 && index < 3)
        {
            itemList[index] = item;
            OnModelChange?.Invoke(GetCurrentState());
            return true;
        }
        else if (index == 2)
        {

        }

        return false;
    }

    public Dictionary<int, Item> GetCurrentState()
    {
        return itemList;
    }
}
