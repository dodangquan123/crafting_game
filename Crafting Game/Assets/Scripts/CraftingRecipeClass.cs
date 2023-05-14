using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCraftingRecipe", menuName = "Crafting/Recipe")]
public class CraftingRecipeClass : ScriptableObject
{
    public Item[] inputItems;
    public Item outputItem;

    // public bool CanCraft(InventoryManager inventory){
    //     for (int i = 0; i < inputItems.Length; i++){
    //         if (!inventory.ContainsItemForRecipe(inputItems[i].GetItem(), inputItems[i].GetQuantity()))
    //             return false; 
    //     }
    //     return true;
    // }
    // public void Craft(InventoryManager inventory){
    //     // remove the input items from the inventory
    //     for (int i = 0; i < inputItems.Length; i++){
    //         inventory.RemoveItemForCrafting(inputItems[i].GetItem(), inputItems[i].GetQuantity());
    //     }
    //     // add the output item into
    // }

    public Item[] GetInputItems(){
        return inputItems;
    }

    public Item GetOutputItem(){
        return outputItem;
    }
}
