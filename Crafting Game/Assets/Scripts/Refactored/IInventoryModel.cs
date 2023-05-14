using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryModel
{
    public Item GetData(int index);
    public bool SetData(int index, Item item);
    public bool HandleSwapItem(Item item, int index);
    public bool HandleAddItem(Item item, int index);
}
