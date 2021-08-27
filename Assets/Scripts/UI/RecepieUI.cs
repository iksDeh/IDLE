using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RecepieUI : MonoBehaviour
{
    public Transform content;
    public Font font;
    public Transform craftigSlots;


    CraftingSlot[] slotList;
    RecepieManager recepieManager;
    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;
        recepieManager = RecepieManager.instance;
        recepieManager.onRecepieLearned += UpdateUI;
        slotList = craftigSlots.GetComponentsInChildren<CraftingSlot>();
        
    }

    public void UpdateUI(Recepie recepie)
    {
        GameObject obj = new GameObject(recepie.name);
        Instantiate(obj,new Vector3(0, 0,0), Quaternion.identity);
        obj.transform.SetParent(content);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.layer = LayerMask.NameToLayer("UI");
        Text name = obj.AddComponent<Text>();
        name.text = recepie.name;
        name.fontSize = 20;
        name.font = font;
        name.alignment = TextAnchor.UpperCenter;

        GridLayoutGroup glg = obj.AddComponent<GridLayoutGroup>();
        glg.cellSize = new Vector2( 120, 30);
        glg.padding.top = 30;

        RecepieMaterialUI recepeMaterial = obj.AddComponent<RecepieMaterialUI>();

        ClickableObject clickableObject = obj.AddComponent<ClickableObject>();
        clickableObject.onLeftClick += OnPointerLeftClick;
        clickableObject.onRightClick += OnPointerRightClick;

        foreach (Item i in recepie.materialAmount.Keys)
        {
            GameObject materialName = new GameObject(i.name);
            Instantiate(materialName, new Vector3(0, 0, 0), Quaternion.identity);
            materialName.transform.SetParent(obj.transform);
            materialName.layer = LayerMask.NameToLayer("UI");
            materialName.transform.localScale = new Vector3(1, 1, 1);

            Text materials = materialName.AddComponent<Text>();
            materials.text = i.name;
            materials.font = font;
            materials.alignment = TextAnchor.UpperCenter;


            GameObject materialAmount = new GameObject(recepie.materialAmount[i].ToString());
            Instantiate(materialAmount, new Vector3(0, 0, 0), Quaternion.identity);
            materialAmount.transform.SetParent(obj.transform);
            materialAmount.layer = LayerMask.NameToLayer("UI");
            materialAmount.transform.localScale = new Vector3(1, 1, 1);

            Text amount = materialAmount.AddComponent<Text>();
            amount.text = recepie.materialAmount[i].ToString();
            amount.font = font;

            recepeMaterial.Init(materialName, materialAmount);
            materialName.SetActive(false);
            materialAmount.SetActive(false);
        }
    }

    public void OnPointerLeftClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject gobject = eventData.pointerCurrentRaycast.gameObject;
            gobject.GetComponent<RecepieMaterialUI>().SetActiv(!gobject.GetComponent<RecepieMaterialUI>().activSelf);

        }
    }
    public void OnPointerRightClick(PointerEventData eventData)
    {
         if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (inventory.Remove(recepieManager.GetRecepie(eventData.pointerCurrentRaycast.gameObject.name).materialAmount))
            {
                GameObject gobject = eventData.pointerCurrentRaycast.gameObject;
                CraftingLayout cl;
                cl = recepieManager.GetRecepie(gobject.name).craftingData;
                int counter = 0;
                for (int i = 0; i < cl.rows.Length; i++)
                {
                    for (int j = 0; j < cl.rows[i].row.Count; j++)
                    {
                        slotList[counter].AddItem(cl.rows[i].row[j]);
                        counter++;
                    }
                }
            }
            else
                Debug.Log("Not enough Items");

        }
    }
}
