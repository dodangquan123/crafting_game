using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryControler : MonoBehaviour
{
    [SerializeField] private InventoryModel inventoryModel;
    [SerializeField] private UIInventory inventoryUI;
    private MouseFollowerModel mouseFollowerModel;
    [SerializeField] private UIMouseFollower mouseFollowerUI;
    [SerializeField] private List<Item> itemToAddList;
    public void Start()
    {
        inventoryModel.Initialize(itemToAddList);
        inventoryUI.Initialize(inventoryModel);
        mouseFollowerModel = new MouseFollowerModel();
        mouseFollowerUI.Initialize(mouseFollowerModel);

        inventoryUI.OnEndDragging += HanleEndDragging;
        inventoryUI.OnDropOnInventory += HandleDropOnInventory;
        inventoryUI.OnStartDragging += HandleStartDragging;
    }
    private void HanleEndDragging()
    {
        // Remove item and disable the visual of the item drag
        mouseFollowerModel.EndDrag();
    }
    public void HandleStartDragging(IInventoryModel model, int index)
    {
        // Set data to update the visual of the item beging drag
        mouseFollowerModel.SetData(model, index);
    }
    public void HandleDropOnInventory(IInventoryModel model,int index)
    {
        //TODO: 3 steps
        // 1: if no item on des, try to move the source item over, display warning if failed
        // 2: if there is item on des, and diff items, try to swaps, if failed revert back
        // 3: if same item, add item from source to des, and subtract that amount from source.
        // rename functions.
        mouseFollowerModel.SetData(model, index);
        Item sourceItem = mouseFollowerModel.GetSourceItem();
        IInventoryModel sourceModel = mouseFollowerModel.GetSourceModel();
        int sourceIndex = mouseFollowerModel.GetSourceIndex();
        Item destinationItem = model.GetData(index);
        IInventoryModel destinationModel = model;
        int destinationIndex = index;
        bool result = true;
        if (destinationItem == null || !sourceItem.IsSameItemClass(destinationItem))
        {
            result &= destinationModel.HandleSwapItem(sourceItem, destinationIndex);
            result &= sourceModel.HandleSwapItem(destinationItem, sourceIndex);
            // TODO handle when error
            if (!result)
            {
                destinationModel.HandleSwapItem(destinationItem, destinationIndex);
                sourceModel.HandleSwapItem(sourceItem, sourceIndex);
            }
        }
        else if (sourceItem.IsSameItemClass(destinationItem)) {
            destinationModel.HandleAddItem(sourceItem, destinationIndex);
            if (sourceItem.GetCount() > 0)
            {
                sourceModel.HandleSwapItem(sourceItem, sourceIndex);
            }
            else
            {
                sourceModel.HandleSwapItem(null, sourceIndex);
            }
        }
    }
}
