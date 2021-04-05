/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-04-04 17:20:41 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-04-04 21:04:52
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public InventoryController inventoryController;
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    
    public int index;
    private int slotRange = 100;

    private Vector2 initPosition;
    private void Awake() {
        inventoryController = FindObjectOfType<InventoryController>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        initPosition = eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition;
    }
    
    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");

        Vector2 finalPosition = eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition;

        // Only when the item is dragged outside of the slot, the item will be used; otherwise, it will snap back to the slot.
        if (Vector2.Distance(finalPosition, initPosition) > slotRange)
        {
            inventoryController.UseItem(index);
        }

        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = initPosition;
    }

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("OnPointerDown");
    }
}
