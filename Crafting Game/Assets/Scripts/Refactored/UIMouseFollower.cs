using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMouseFollower : MonoBehaviour
{
    private MouseFollowerModel model;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private UIItemSlot slot;

    public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        mainCamera = Camera.main;
        //slot = GetComponentInChildren<UIItemSlot>();
    }

    public void Initialize(MouseFollowerModel model) { 
        // Add model and observe model's event
        this.model = model;
        this.model.OnDataChanged += this.SetData;
        gameObject.SetActive(false); 
    }

    public void SetData(Item item)
    {
        // Set visual for mousefollower using slot UI, set active if item is not null
        slot.SetData(item);
        if (item != null )
        {
            Toggle(true);
        }
        else { Toggle(false); }
    }

    public void Update()
    {
        // Have no idea how it work but it update the location to follow the mouse
        Vector2 possition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition,
            canvas.worldCamera,
            out possition);
        transform.position = canvas.transform.TransformPoint(possition);
    }

    public void Toggle(bool val)
    {
        // Updata the location before display it
        Update();
        gameObject.SetActive(val);
    }
}
