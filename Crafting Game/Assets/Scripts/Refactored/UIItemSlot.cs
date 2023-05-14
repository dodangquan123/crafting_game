using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIItemSlot : MonoBehaviour, IPointerClickHandler,
    IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private Item item;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image icon;
    [SerializeField] private Text amount;
    [SerializeField] private Image condition;

    public event Action<UIItemSlot> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag ;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        RefreshSlot();
    }

    public void SetData(Item item)
    {
        this.item = item;
        RefreshSlot();
    }

    public void RefreshSlot()
    {
        UpdateIcon();
        UpdateCount();
        UpdateConditionBar();
        
    }
    private void UpdateIcon()
    {
        if (item == null)
        {
            icon.enabled = false;
        }
        else
        {
            icon.enabled = true;
            icon.sprite = item.GetItemIcon();
        }
    }
    private void UpdateCount()
    {
        if (item == null) {
            amount.enabled = false;
            return;
        }
        int count = item.GetCount();
        if (count > 1)
        {
            amount.enabled = true;
            amount.text = count.ToString();
        }
        else
        {
            amount.enabled = false;
        }
    }
    private void UpdateConditionBar()
    {
        if (item == null || !item.IsDegradable())
        {
            condition.enabled = false;
        }
        else
        {
            condition.enabled = true;
            float conditionPercent = item.GetConditionInPercentage();
            float barWidth = rectTransform.rect.width * item.GetConditionInPercentage();
            condition.rectTransform.sizeDelta = new Vector2(barWidth, condition.rectTransform.sizeDelta.y);
            condition.color = Color.Lerp(Color.red, Color.green, conditionPercent);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item == null) { return; }
        OnItemClicked?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // User start dragging item in the slot
        if (item == null) { return; }
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // User end dragging item in the slot
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        // User drop item on the slot
        OnItemDroppedOn?.Invoke(this);
    }
}
