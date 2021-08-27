using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    public GameObject playerUI;

    private PlayerStats playerStats;
    private Slider sliderHP;
    TextMeshProUGUI tmHP;

    void Start()
    {
        foreach (TextMeshProUGUI tm in playerUI.GetComponentsInChildren<TextMeshProUGUI>())
            if (tm.transform.name == "HPNumber")
                tmHP = tm;



        playerStats = this.GetComponent<PlayerStats>();
        sliderHP = playerUI.GetComponentInChildren<Slider>();

        playerStats.OnDamageTaken += UpdateHP;
        sliderHP.minValue = 0;
        UpdateHP();
    }

    public void UpdateHP()
    {
        tmHP.text = playerStats.currentHealth.ToString() + " / " + playerStats.maxHealth.ToString();
        sliderHP.maxValue = playerStats.maxHealth;
        sliderHP.value = playerStats.currentHealth;
    }
}
