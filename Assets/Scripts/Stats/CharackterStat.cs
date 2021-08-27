using UnityEngine;

public class CharackterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;
    public Stat stammina;
    public Stat agility;
    public Stat strength;
    public Stat inttiligenz;
    public Stat spirit;

    public event System.Action OnDamageTaken;

    void Awake()
    {
        currentHealth = maxHealth;  
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
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
