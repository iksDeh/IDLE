using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform rect;
    private CanvasGroup canvasGroup;
    private Vector2 posAnchored;
    private InventorySlot invSlot;
    private void Awake()
    { 
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        invSlot = GetComponent<InventorySlot>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.transform.position = PlayerController.instance.mousePos;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if(eventData.pointerDrag.GetComponent<CraftingSlot>()  == null)
        {
            rect.anchoredPosition = posAnchored;
        }
        else if(eventData.pointerDrag.GetComponent<InventorySlot>() == null)
        {
            rect.anchoredPosition = posAnchored;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       
        posAnchored = rect.anchoredPosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("test");
    }
}
