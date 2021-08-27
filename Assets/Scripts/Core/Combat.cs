using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharackterStats))]
public class Combat : MonoBehaviour
{
    public CharackterStats myStats;
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackDelay = .6f;

    public event System.Action OnAttack;

    void Start()
    {
        myStats = GetComponent<CharackterStats>();
    }

    public virtual void Attack(CharackterStats targetStats)
    {
            if(attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null)
                OnAttack();

            attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage(CharackterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.damage.GetValue());
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;
    }
}
