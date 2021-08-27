using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class CraftingSlot : MonoBehaviour, IDropHandler
{


    Item item;
    public Sprite defaulSprite;
    public Image image;
    private int amount;
    private InventorySlot invSlot;
    public Item GetItem()
    {
        return item;
    }

    public void Remove()
    {
        item = null;
        image.sprite = defaulSprite;
        amount = 0;
        invSlot = null;
    }

    public void AddItem(Item item)
    {
        if(item != null)
        {
            this.item = item;
            image.sprite = item.icon;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            invSlot = eventData.pointerDrag.GetComponent<InventorySlot>();
            item = invSlot.GetItem();
            image.sprite = invSlot.icon.sprite;


            if (invSlot.tm.text != "")
            {
                amount = Convert.ToInt32(invSlot.tm.text) - 1;
                if (amount > 1)
                {
                    eventData.pointerDrag.GetComponent<InventorySlot>().tm.text = amount.ToString();
                }
                else
                    eventData.pointerDrag.GetComponent<InventorySlot>().tm.text = "";
            }
            else
            {
                eventData.pointerDrag.GetComponent<CanvasGroup>().alpha = 1f;
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                eventData.pointerDrag.GetComponent<Transform>().gameObject.SetActive(false);
            }
            Inventory.instance.Remove(item);
            

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

        }
    }
}
