using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private int id;
    public static event Action<int> OnItemChanged;

    public Item GetItem() {
        if (transform.childCount == 0) return null;
        InventoryItem inventoryItem = GetComponentInChildren<InventoryItem>();
        return inventoryItem.GetItem();
    }
    public void OnDrop(PointerEventData eventData){
        if (transform.childCount == 0){
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.OnItemBeginDrag += OnItemBeginDrag;
            inventoryItem.SetParentAfterDrag(transform);
            inventoryItem.transform.localScale = transform.localScale;
            OnItemChanged?.Invoke(id);
        }
    }

    public void OnItemBeginDrag(InventoryItem inventoryItem){
        inventoryItem.OnItemBeginDrag -= OnItemBeginDrag;
        OnItemChanged?.Invoke(id);
    }
}
