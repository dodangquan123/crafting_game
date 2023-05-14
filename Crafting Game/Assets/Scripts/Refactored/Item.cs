using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[System.Serializable]
public class Item
{
    [SerializeField] private ItemClass itemClass;
    [SerializeField] private int count = 0;
    [SerializeField] private int condition = -1;
    public Item(Item other)
    {
        this.itemClass = other.itemClass;
        this.count = other.count;
        this.condition = other.condition;
    }

    public Item(ItemClass itemClass, int count = 1, int condition = -1)
    {
        this.itemClass = itemClass;
        this.count = count;
        this.condition = condition;
    }
    public Item()
    {

    }

    public bool IsDegradable()
    {
        return this.condition >= 0;
    }

    public int GetCondition() { return this.condition; }

    public float GetConditionInPercentage()
    {
        return (float)this.condition / itemClass.GetMaxCondition();
    }
    public void SetCondition(int condition) { this.condition = Math.Min(condition, itemClass.GetMaxCondition()); }
    public ItemClass GetItemClass(){
        return itemClass;
    }

    public int GetCount(){
        return count;
    }

    public void AddCount(int count)
    {
        this.count += count;
    }

    public void SubCount(int count)
    {
        this.count -= count;
    }

    public ItemClass.ItemType GetItemType(){
        return itemClass.GetItemType();
    }

    public Sprite GetItemIcon(){
        return itemClass.GetItemIcon();
    }

    public int GetMaxStack(){
        return itemClass.GetMaxStack();
    }
    public bool IsStackable() {
        return itemClass.IsStackable();
    }

    public bool IsAddable (Item item){
        return this.itemClass == item.GetItemClass() && 
                this.itemClass.IsStackable() && 
                this.count < itemClass.GetMaxStack();
    }

    public static bool Compare(Item item1, Item item2)
    {
        return item1.IsSameItemClass(item2) && item1.condition == item2.condition;
    }


    public bool IsSameItemClass(Item item)
    {
        if (item == null) return false;
        return this.itemClass == item.itemClass;
    }

    public bool CanUseAsIngredient(Item ingredient)
    {
        return this.itemClass == ingredient.itemClass && this.count >= ingredient.count;
    }

    public int AddSameItem(Item other)
    {
        if (other == null) return 0;
        if (!IsSameItemClass(other)) return 0;
        if (!itemClass.IsStackable()) return 0;
        int addableAmount = math.min(this.itemClass.GetMaxStack() - count, other.GetCount());
        this.AddCount(addableAmount);
        other.SubCount(addableAmount);
        return addableAmount;
    }
}
