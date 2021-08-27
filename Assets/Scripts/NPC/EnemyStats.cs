using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharackterStats
{

    public override void Die()
    {
        base.Die();

        PlayerController.instance.EnemyKilled(this.GetComponent<Enemy>().questIDs); ;
        EnemyManager.instance.RemoveEnemy(this.GetComponent<Enemy>());
        Destroy(gameObject);
    }
}
