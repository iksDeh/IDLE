using UnityEngine;
using System.Collections.Generic;

public class CharackterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public StatList stats;


    private int currentStats = 0;
    private int maxStats;


    public event System.Action OnDamageTaken;

    public delegate void OnLevelUp();
    public OnLevelUp onLevelUp;
    void Awake()
    {      
        currentHealth = maxHealth;
        foreach(Stat stat in stats.stats)
        {
            currentStats += stat.GetValue();
        }

        maxStats = currentStats;        
        onLevelUp += LevelUp;
    }

    public void AddModifier(Stat stat, int value)
    {
        currentStats += value;
        stat.AddModifier(value);
    }

    public void RemoveModifier(Stat stat, int value)
    {
        currentStats -= value;
        stat.RemoveModifier(value);
    }

    public int GetAvilableStats()
    {
        return maxStats - currentStats;
    }

    private void LevelUp()
    {
        maxStats += stats.level.lvlUpStatAmount;
        Debug.Log(currentStats + " | " + maxStats);
    }

    public void TakeDamage(int damage)
    {
        
        damage -= stats.armor.GetValue();
        if (damage < 0) damage = 0;

        currentHealth -= damage;

        if (OnDamageTaken != null)
            OnDamageTaken();

        if (currentHealth <= 0)
            Die();
    }

    public void SetHealth(int addHealth)
    {
        maxHealth += addHealth;
        currentHealth += addHealth;

    }
    public virtual void Die()
    {

    }
}
