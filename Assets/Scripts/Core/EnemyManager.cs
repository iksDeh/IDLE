using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region

    public static EnemyManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public float respawnDelay = 1000;
    public Transform spawnAreas;
    List<Enemy> allEnemyList;

    
    private int id = 0;


    void Start()
    {
        allEnemyList = new List<Enemy>();

        foreach (SpawnAreas area in spawnAreas.GetComponentsInChildren<SpawnAreas>())
        {
            for (id = area.GetEnmyCountInArea(); id <= area.enemyAmountInArea; id++)
                foreach (Enemy enemy in area.enemyTyp)
                    if (area.GetEnmyCountInArea() <= area.enemyAmountInArea)
                    {
                        GameObject newObj;
                        Enemy e;
                        newObj = Instantiate(enemy.gameObject, area.GetSpawnPoint(), Quaternion.identity);
                        newObj.transform.name = enemy.name;
                        newObj.SetActive(true);
                        newObj.transform.SetParent(area.transform);
                        e = newObj.GetComponent<Enemy>();
                        e.id = enemy.id;
                        e.isActiveQuestMob = enemy.isActiveQuestMob;
                        e.questIDs = enemy.questIDs;
                        area.AddEnemy(e);
                        AddEnemy(e);
                    }
        }

        StartCoroutine(SpawnEnemy(respawnDelay));
    }

    public void AddEnemy(Enemy enemy)   
    {
        allEnemyList.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {

        allEnemyList.Remove(enemy);
    }

    public void SetQuestEnemy(Enemy enemy, int value)
    {

        enemy.isActiveQuestMob = true;
        foreach (SpawnAreas area in spawnAreas.GetComponentsInChildren<SpawnAreas>())
        {
            foreach (Enemy en in area.enemyTyp)
                if (enemy.id == en.id)
                    en.questIDs.Add(value);
            foreach (Enemy en in area.enemys)
                if (enemy.id == en.id)
                    en.questIDs.Add(value);
        }
    }

    IEnumerator SpawnEnemy(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            foreach (SpawnAreas area in spawnAreas.GetComponentsInChildren<SpawnAreas>())
            {
                for (id = area.GetEnmyCountInArea(); id <= area.enemyAmountInArea; id++)
                    foreach (Enemy enemy in area.enemyTyp)
                        if (area.GetEnmyCountInArea() <= area.enemyAmountInArea)
                        {
                            GameObject newObj;
                            Enemy e;
                            newObj = Instantiate(enemy.gameObject, area.GetSpawnPoint(), Quaternion.identity);
                            newObj.transform.name = enemy.name;
                            newObj.SetActive(true);
                            newObj.transform.SetParent(area.transform);
                            e = newObj.GetComponent<Enemy>();
                            e.id = enemy.id;
                            e.isActiveQuestMob = enemy.isActiveQuestMob;
                            e.questIDs = enemy.questIDs;
                            area.AddEnemy(e);
                            AddEnemy(e);
                        }
            }
        }
    }
}
