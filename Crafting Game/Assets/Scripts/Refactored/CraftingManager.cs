using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] private IngredientSlot[] ingredientSlotList;
    [SerializeField] private ToolSlot toolSlot;
    [SerializeField] private DisplaySlot resultSlot;
    [SerializeField] private Button craftButton;
    private Item[] itemList;
    [SerializeField] private GameObject displayItemPrefab;
    [SerializeField] private CraftingRecipeClass[] recipeList;
    [SerializeField] private CraftingRecipeClass recipe;
    
    private void Awake(){
        itemList = new Item[ingredientSlotList.Length];
        //for (int i = 0; i < itemList.Length; i++) itemList[i] = null;
        recipeList = Resources.LoadAll<CraftingRecipeClass>("Recipes");
        IngredientSlot.OnItemChanged += HandleItemChanged;
    }

    public void HandleItemChanged(int index){
        itemList[index] = ingredientSlotList[index].GetItem();
        CanCraft();
    }

    public void CraftItem()
    {
        if (!CanCraft()) return ;
        for (int i = 0; i < itemList.Length; i++) 
        {
            foreach (Item ingredientItem in recipe.GetInputItems())
                if (itemList[i] != null && itemList[i].CanUseAsIngredient(ingredientItem))
                {
                    //itemList[i].SubCount(ingredientItem.GetCount());
                    if (itemList[i].GetCount() <= 0) itemList[i] = null;
                    break;
                }
        }
        //inventoryManager.AddItem(new Item(recipe.GetOutputItem()));
        CanCraft();
    }

    public bool CanCraft(){
        int numberOfInput = GetNumberOfInput();
        Item[] ingredientList;
        foreach (CraftingRecipeClass recipe in recipeList){
            ingredientList = recipe.GetInputItems();
            if (numberOfInput != ingredientList.Length) continue;
            if (HasAllItemsForRecipe(ingredientList))
            {
                if (this.recipe == null)
                {
                    this.recipe = recipe;
                    SpawnNewDisplayItem(recipe.GetOutputItem());
                }
                if (this.recipe != recipe)
                {
                    this.recipe = recipe;
                    DestroyDisplayItem();
                    SpawnNewDisplayItem(recipe.GetOutputItem());
                }

                return true;
            }
        }
        if (recipe != null)
        {
            recipe = null;
            DestroyDisplayItem();
        }
        return false;
    }

    private int GetNumberOfInput()
    {
        int result = 0;
        foreach (Item item in itemList)
            if (item != null)
                result++;
        return result;
    }

    private bool HasAllItemsForRecipe(Item[] ingredientList)
    {
        foreach (Item ingredientItem in ingredientList) { 
            bool result = false;
            foreach (Item item in itemList)
                if (item != null && item.CanUseAsIngredient(ingredientItem))
                {
                    result = true;
                    break;
                }
            if (!result)
                return false;
        }
        return true;
    }

    public void SpawnNewDisplayItem(Item item)
    {
        GameObject newItemGameObject = Instantiate(displayItemPrefab, resultSlot.transform);
        DisplayItem displayItem = newItemGameObject.GetComponent<DisplayItem>();
        displayItem.Initialize(item);
    }

    public void DestroyDisplayItem()
    {
        DisplayItem displayItem = resultSlot.gameObject.GetComponentInChildren<DisplayItem>();
        if (displayItem != null)
            Destroy(displayItem.gameObject);
    }

}
