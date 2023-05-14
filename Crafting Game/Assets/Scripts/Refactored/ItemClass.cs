using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class ItemClass : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private bool isStackable = true;
    [SerializeField] private ItemType itemType;
    [SerializeField] private int maxStack = 64;
    [SerializeField] private int maxCondition = -1;

    public string GetItemName(){
        return itemName;
    }

    public Sprite GetItemIcon(){
        return itemIcon;
    }

    public bool IsStackable(){
        return isStackable;
    }

    public ItemType GetItemType(){
        return itemType;
    }

    public int GetMaxStack(){
        return maxStack;
    }

    public int GetMaxCondition()
    {
        return maxCondition;
    }

    public enum ItemType {
        Consumable,
        Tool,
        Misc
    }
}