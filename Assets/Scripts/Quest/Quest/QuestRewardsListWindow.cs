using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestRewardsListWindow : MonoBehaviour
{
    public GameObject questReward;
    public GameObject questRewardAmount;
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
            GameObject image;
            image = Instantiate(questReward, new Vector3(0, 0, 0), Quaternion.identity);
            image.transform.SetParent(this.transform);
            image.transform.localScale = new Vector3(1, 1, 1);
            image.layer = LayerMask.NameToLayer("UI");
            image.SetActive(true); 

            Image img = image.GetComponent<Image>();
            img.sprite = i.icon;

            TextMeshProUGUI amo = image.GetComponentInChildren<TextMeshProUGUI>();
            amo.text = questRewards[i].ToString();
            amo.fontSize = 4;
            amo.fontSharedMaterial = font;
            amo.alignment = TextAlignmentOptions.BottomRight;

            createdObjects.Add(image);

            counter++;
        }      
    }  
}
