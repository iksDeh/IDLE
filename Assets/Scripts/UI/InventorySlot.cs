using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image icon;
    public Button removeButton;
    public GameObject inventorySlot;
    public Image background;
    public TextMeshProUGUI tm;
    private ClickableObject button;
    Item item;

    public Transform rightClickMenu;
    private InventoryRightClickMenu[] menuButtons;

    void Start()
    {
        menuButtons = rightClickMenu.GetComponentsInChildren<InventoryRightClickMenu>();
        for (int i = 0; i < menuButtons.Length; i++)
            if (menuButtons[i].invButtons == InventoryButtons.Use)
                menuButtons[i].onLeftClick += UseItem;

        button = this.GetComponent<ClickableObject>();
        button.onLeftClick += UseItem;
        button.onRightClick += RightClick;
    }
    public Item GetItem()
    {
        return item;
    }
    public void AddItem(Item newItem)
    {
        if (Inventory.instance.itemList[newItem] > 1)
        {
            if(icon.sprite != null)
                tm.text = Inventory.instance.itemList[newItem].ToString();
            else
            {
                item = newItem;

                inventorySlot.SetActive(true);
                background.enabled = true;

                tm.text = Inventory.instance.itemList[newItem].ToString();
                icon.sprite = item.icon;
                icon.enabled = true;
                removeButton.interactable = true;
            }
        }
        else
        {
            item = newItem;

            inventorySlot.SetActive(true);
            background.enabled = true;

            tm.text = "";
            icon.sprite = item.icon;

            icon.enabled = true;
            removeButton.interactable = true;
        }
    }

    public void ClearSlot()
    {
        inventorySlot.SetActive(false);
        background.enabled = false ;

        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item, Inventory.instance.GetItemAmount(item));
        ClearSlot();
    }

    public void Reset()
    {
        rightClickMenu.gameObject.SetActive(false);
    }

    public void RightClick(PointerEventData eventData)
    {
        Vector2 anchoredPosition = menuButtons[0].rect.anchoredPosition;
        anchoredPosition.x += 40f;
        anchoredPosition.y -= 60f;

        rightClickMenu.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        rightClickMenu.gameObject.SetActive(!rightClickMenu.gameObject.activeSelf);
    }

    public void UseItem(PointerEventData eventData)
    {
        if (item != null)
        {
            item.Use();
            rightClickMenu.gameObject.SetActive(false);
        }

    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("on INv drop");
        if(eventData.pointerDrag != null)
        {
            Inventory.instance.Add(eventData.pointerDrag.GetComponent<CraftingSlot>().GetItem());
            eventData.pointerDrag.GetComponent<CraftingSlot>().Remove();
            eventData.pointerDrag.GetComponent<CanvasGroup>().alpha = 1f;
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

        }
    }
}
