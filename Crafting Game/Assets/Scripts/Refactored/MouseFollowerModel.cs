using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowerModel
{

    private IInventoryModel sourceModel;
    private IInventoryModel destinationModel;
    private int sourceIndex;
    private int destinationIndex;

    public event Action<Item> OnDataChanged;

    public MouseFollowerModel() { }
    public void SetData(IInventoryModel model, int index)
    {
        // Set the source inventory model and its index for tracking
        Item item = model.GetData(index);
        if (item == null) return;
        if (this.sourceModel == null)
        {
            this.sourceModel = model;
            this.sourceIndex = index;
            OnDataChanged?.Invoke(GetData());
        }
        else
        {
            this.destinationModel = model;
            this.destinationIndex = index;
        }
    }

    public Item GetData() { return sourceModel?.GetData(sourceIndex); }

    public void EndDrag()
    {
        sourceModel = null;
        destinationModel = null;
        sourceIndex = -1;
        destinationIndex = -1;
        OnDataChanged?.Invoke(GetData());
    }

    public IInventoryModel GetSourceModel()
    {
        return sourceModel;
    }

    public int GetSourceIndex()
    {
        return sourceIndex;
    }

    public Item GetSourceItem()
    {
        return sourceModel?.GetData(sourceIndex);
    }
}
