using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestRewardsListWindow : MonoBehaviour
{
    public Material font;
    private List<GameObject> createdObjects;
    private Dictionary<Item, int> questRewards;
    public void RemoveAllRewards()
    {
        if(createdObjects != null)
        foreach(GameObject o in createdObjects) 
        {
            Destroy(o);
        }
    }

    public void SetReward(Quest q)
    {
        createdObjects = new List<GameObject>();
        int counter = 0;
        questRewards = q.questRewards.GetItems();
        foreach (Item i in questRewards.Keys)
        {

            GameObject image = new GameObject(i.name);
            Instantiate(image, new Vector3(0, 0, 0), Quaternion.identity);
            image.transform.SetParent(this.transform);
            image.transform.localScale = new Vector3(1, 1, 1);
            image.layer = LayerMask.NameToLayer("UI");

            GridLayoutGroup glg = image.AddComponent<GridLayoutGroup>();
            glg.cellSize = this.GetComponent<GridLayoutGroup>().cellSize;

            Image img = image.AddComponent<Image>();
            img.sprite = i.icon;

            GameObject amount = new GameObject(i.name);
            Instantiate(amount, new Vector3(0, 0, 0), Quaternion.identity);
            amount.transform.SetParent(image.transform);
            amount.transform.localScale = new Vector3(1, 1, 1);
            amount.layer = LayerMask.NameToLayer("UI");

            TextMeshProUGUI amo = amount.AddComponent<TextMeshProUGUI>();
            amo.text = questRewards[i].ToString();
            amo.fontSize = 4;
            amo.fontSharedMaterial = font;
            amo.alignment = TextAlignmentOptions.BottomRight;

            createdObjects.Add(image);
            createdObjects.Add(amount);

            counter++;
        }
        //if (q.questRewards.currencyRewards != null)
        //{
        //  //  if (q.questRewards.currencyRewards.copper > 0)
        //    {


        //        GameObject image = new GameObject("CopperIMG");
        //        Instantiate(image, new Vector3(0, 0, 0), Quaternion.identity);
        //        image.transform.SetParent(this.transform);
        //        image.transform.localScale = new Vector3(1, 1, 1);
        //        image.layer = LayerMask.NameToLayer("UI");

        //        GridLayoutGroup glg = image.AddComponent<GridLayoutGroup>();
        //        glg.cellSize = this.GetComponent<GridLayoutGroup>().cellSize;

        //        Image img = image.AddComponent<Image>();
        //      //  img.sprite = Inventory.instance.currency.imageCopper;

        //        GameObject amount = new GameObject("CopperAmount");
        //        Instantiate(amount, new Vector3(0, 0, 0), Quaternion.identity);
        //        amount.transform.SetParent(image.transform);
        //        amount.transform.localScale = new Vector3(1, 1, 1);
        //        amount.layer = LayerMask.NameToLayer("UI");

        //        TextMeshProUGUI amo = amount.AddComponent<TextMeshProUGUI>();
        //     //   amo.text = q.questRewards.currencyRewards.copper.ToString();
        //        amo.fontSize = 4;
        //        amo.fontSharedMaterial = font;
        //        amo.alignment = TextAlignmentOptions.BottomRight;

        //        createdObjects.Add(image);
        //        createdObjects.Add(amount);
        //    }
        //}
    }

    public void SetCurrency(Quest q)
    {
        //Currency HINZUFÜGEN

        //if (q.questRewards.currencyRewards != null)
        //{
        // //   if(q.questRewards.currencyRewards.copper > 0)
        //    {
                

        //        GameObject image = new GameObject("CopperIMG");
        //        Instantiate(image, new Vector3(0, 0, 0), Quaternion.identity);
        //        image.transform.SetParent(this.transform);
        //        image.transform.localScale = new Vector3(1, 1, 1);
        //        image.layer = LayerMask.NameToLayer("UI");

        //        Image img = image.AddComponent<Image>();
        //  //      img.sprite = Inventory.instance.currency.imageCopper;

        //        GameObject amount = new GameObject("CopperAmount");
        //        Instantiate(amount, new Vector3(0, 0, 0), Quaternion.identity);
        //        amount.transform.SetParent(this.transform);
        //        amount.transform.localScale = new Vector3(1, 1, 1);
        //        amount.layer = LayerMask.NameToLayer("UI");

        //        TextMeshProUGUI amo = amount.AddComponent<TextMeshProUGUI>();
        //    //    amo.text = q.questRewards.currencyRewards.copper.ToString();
        //        amo.fontSize = 4;
        //        amo.fontSharedMaterial = font;

        //        createdObjects.Add(image);
        //        createdObjects.Add(amount);
        //    }
        //}
    }

        
        //if(q.questRewards.currencyRewards.copper != 0 )

        //{

        //    GameObject image = new GameObject(i.item.name);
        //    Instantiate(image, new Vector3(0, 0, 0), Quaternion.identity);
        //    image.transform.SetParent(this.transform);
        //    image.transform.localScale = new Vector3(1, 1, 1);
        //    image.layer = LayerMask.NameToLayer("UI");

        //    Image img = image.AddComponent<Image>();
        //    img.sprite = i.item.icon;

        //    GameObject amount = new GameObject(i.item.name);
        //    Instantiate(amount, new Vector3(0, 0, 0), Quaternion.identity);
        //    amount.transform.SetParent(this.transform);
        //    amount.transform.localScale = new Vector3(1, 1, 1);
        //    amount.layer = LayerMask.NameToLayer("UI");

        //    TextMeshProUGUI amo = amount.AddComponent<TextMeshProUGUI>();
        //    amo.text = i.amount.ToString();
        //    amo.fontSize = 4;
        //    amo.fontSharedMaterial = font;

        //    createdObjects.Add(image);
        //    createdObjects.Add(amount);

        //    counter++;
        //}
   
}
