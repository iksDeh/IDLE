using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyUI : MonoBehaviour
{
    public GameObject enemyUI;

    EnemyStats enemyStats;
    Slider slider;
    TextMeshProUGUI tmHP;
    TextMeshProUGUI tmName;

    int hp = 0;

    void Start()
    {
        enemyStats = this.GetComponent<EnemyStats>();
        slider = enemyUI.GetComponentInChildren<Slider>();
        foreach (TextMeshProUGUI tm in enemyUI.GetComponentsInChildren<TextMeshProUGUI>())
            if (tm.transform.name == "HPNumber")
                tmHP = tm;
            else if (tm.transform.name == "EnemyName")
                tmName = tm;

        tmHP.text = enemyStats.currentHealth.ToString() + " / " + enemyStats.maxHealth.ToString();
        tmName.text = this.transform.name;

        slider.minValue = 0;

        enemyStats.OnDamageTaken += UpdateHP;
        UpdateHP();
    }


    public void UpdateHP()
    {
        tmHP.text = enemyStats.currentHealth.ToString() + " / " + enemyStats.maxHealth.ToString();

        slider.maxValue = enemyStats.maxHealth;
        slider.value = enemyStats.currentHealth;

        hp = enemyStats.currentHealth;
    }

}
