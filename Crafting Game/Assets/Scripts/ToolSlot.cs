using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolSlot : InventorySlot
{
    private ItemClass.ItemType allowedType = ItemClass.ItemType.Tool;
    public override void OnDrop(PointerEventData eventData){
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        ItemClass.ItemType type = inventoryItem.GetItemType();
        if (type == allowedType)
            base.OnDrop(eventData);
    }
}
