using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class InventoryItem : DisplayItem, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private Transform parentAfterDrag;
    public event Action<InventoryItem> OnItemBeginDrag;

    public Item GetItem(){
        return item;
    }

    public ItemClass.ItemType GetItemType(){
        return item.GetItemType();
    }

    public void OnBeginDrag(PointerEventData eventData){
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData){
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

    public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }

    public void SetParentAfterDrag(Transform parentAfterDrag){
        this.parentAfterDrag = parentAfterDrag;
        transform.SetParent(parentAfterDrag);
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        item.AddSameItem(inventoryItem.GetItem());
    }
}
