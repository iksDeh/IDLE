using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerStatsUI : MonoBehaviour
{
    public CharackterStats stats;

    public GameObject statTransform;
    public Transform statParent;
    public Material font;
    public TextMeshProUGUI tmName;
    public TextMeshProUGUI tmAmount;
    public TextMeshProUGUI tmAvilablePoints;

    public Button btnRaise;
    public Button btnLower;

    private List<GameObject> objList;
    private Dictionary<Stat, TextMeshProUGUI> statAmount;


    int attributePointsCache = 0;
    void Start()
    {
        stats.onLevelUp += UpdateUI;
    }

    public void SaveAttributes()
    {
        attributePointsCache = 0;
        this.gameObject.SetActive(false);
    }

    public void LowerStats(Button btn)
    {
        if (attributePointsCache > 0)
        {
            foreach (Stat stat in stats.stats.stats)
                if (stat.statName.ToString() == btn.name)
                {
                    stats.RemoveModifier(stat, 1);
                    attributePointsCache--;
                    UpdateUI();
                    return;
                }

        }
        else
            Debug.Log("No Attribute Points avilable");
    }
    public void RaiseStats(Button btn)
    {
        if (stats.GetAvilableStats() > 0)
        {
        foreach (Stat stat in stats.stats.stats)
            if (stat.statName.ToString() == btn.name)
                {
                    stats.AddModifier(stat, 1);
                    attributePointsCache++;
                    UpdateUI();
                    return;
                }

        }
        else
            Debug.Log("No Attribute Points avilable");
    }

    public void UpdateUI()
    {
        if(this != null)
        {
            tmAvilablePoints.text = "Aviliable Points: " + stats.GetAvilableStats().ToString();

            if (objList != null)
                foreach (Object obj in objList)
                    Destroy(obj);

            objList = new List<GameObject>();
            statAmount = new Dictionary<Stat, TextMeshProUGUI>();

            foreach (Stat stat in stats.stats.stats)
            {
                GameObject obj;
                obj = Instantiate(statTransform);
                obj.transform.SetParent(statParent);
                obj.SetActive(true);
                obj.layer = LayerMask.NameToLayer("UI");
                obj.transform.localScale = new Vector3(1, 1, 1);

                Button btnr = btnRaise;
                btnr.name = stat.statName.ToString();
                Button btnl = btnLower;
                btnl.name = stat.statName.ToString();

                TextMeshProUGUI statName = tmName;
                statName.text = stat.statName.ToString();
                statName.fontSize = 18;
                statName.fontSharedMaterial = font;
                statName.alignment = TextAlignmentOptions.Center;

                TextMeshProUGUI tmStatAmount = tmAmount;
                tmStatAmount.text = stat.GetValue().ToString();
                tmStatAmount.fontSize = 18;
                tmStatAmount.fontSharedMaterial = font;
                statName.alignment = TextAlignmentOptions.MidlineLeft;

                statAmount.Add(stat, tmStatAmount);
                objList.Add(obj);
            }
        }    
    }

    public void OnWindowOpen(InputAction.CallbackContext context)
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        if (this.gameObject.activeInHierarchy)
            UpdateUI();
        else
            SaveAttributes();
    }

}
