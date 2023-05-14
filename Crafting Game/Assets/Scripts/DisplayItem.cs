using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItem : MonoBehaviour
{
    protected Item item;
    protected Image image;
    protected Text countText;

    private void Awake(){
        image = GetComponent<Image>();
        countText = GetComponentInChildren<Text>();
    }

    public void Initialize(Item item){
        this.item = item;
        //item.OnCountChanged += RefreshCount;
        //item.OnDestroyed += Destroy;
        image.sprite = item.GetItemIcon();
        RefreshCount();
    }
    public void RefreshCount(){
        int count = item.GetCount();
        countText.text = count.ToString();
        countText.gameObject.SetActive(count > 1);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void OnDestroy()
    {
        Destroy(image.gameObject);
        Destroy(countText.gameObject);
    }

    public bool IsSameItem(DisplayItem other)
    {
        if (other == null) return false;
        return item.IsSameItemClass(other.item);
    }
}
