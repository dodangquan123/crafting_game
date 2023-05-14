using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    private IInventoryModel inventoryModel;
    [SerializeField] private UIItemSlot itemSlotPrefab;
    [SerializeField] private RectTransform rectTransform;
    List<UIItemSlot> slotList = new List<UIItemSlot>();

    public event Action OnEndDragging;
    public event Action<IInventoryModel, int> OnStartDragging;
    public event Action<IInventoryModel, int> OnDropOnInventory;

    public void Initialize(InventoryModel inventoryModel)
    {
        // Init view, add observe event from slots, and model
        this.inventoryModel = inventoryModel;
        inventoryModel.OnModelChange += this.HandleUpdateView;
        for (int i = 0; i < inventoryModel.GetSize(); i++)
        {
            UIItemSlot slot = Instantiate(itemSlotPrefab);
            slot.transform.SetParent(rectTransform);
            slotList.Add(slot);
            slot.OnItemClicked += HandleItemSelection;
            slot.OnItemBeginDrag += HandleBeginDrag;
            slot.OnItemDroppedOn += HandleDropOn;
            slot.OnItemEndDrag += HandleEndDrag;
            slot.SetData(inventoryModel.GetData(i));
        }
    }

    public void HandleUpdateView(Dictionary<int, Item> inventoryState)
    {
        // Update view by reset all slot and render slot that have item
        foreach (UIItemSlot slot in slotList)
        {
            slot.SetData(null);
        }
        foreach (var slot in inventoryState)
        {
            slotList[slot.Key].SetData(slot.Value);
        }
    }

    private void HandleBeginDrag(UIItemSlot slot)
    {
        // Handle user begin dragging, notify controller
        int index = slotList.IndexOf(slot);
        if (index == -1) return;

        OnStartDragging?.Invoke(inventoryModel, index);
    }


    private void HandleEndDrag(UIItemSlot obj)
    {
        // Handle user stop dragging, notify controller
        OnEndDragging?.Invoke();
    }

    private void HandleDropOn(UIItemSlot slot)
    {
        // Handle user drop on the slot, notify controller
        int index = slotList.IndexOf(slot);
        if (index == -1)
        {
            return;
        }

        OnDropOnInventory?.Invoke(inventoryModel, index);

    }


    private void HandleItemSelection(UIItemSlot obj)
    {
        // Handle user click on slot slot
        throw new NotImplementedException();
    }
}
