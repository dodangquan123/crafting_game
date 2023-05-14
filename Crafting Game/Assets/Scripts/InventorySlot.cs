using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public virtual void OnDrop(PointerEventData eventData){
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.SetParentAfterDrag(transform);
            inventoryItem.transform.localScale = transform.localScale;
        }
    }
}
