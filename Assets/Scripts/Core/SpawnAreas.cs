using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreas : MonoBehaviour
{
    public List<Enemy> enemyTyp;
    [HideInInspector] public List<Enemy> enemys;
    public string name;
    public int enemyAmountInArea = 10;
    float x = 0, y = 0;

    public float height = 10;
    public float weight = 10;
    public Vector3 position = new Vector3();
    public RectTransform rect;

    private void Awake()
    {

    }

    public void SetQuestMobs(Enemy enemy, int value)
    {
        foreach (Enemy en in enemyTyp)
        {
            if (enemy.id == en.id)
                en.questIDs.Add(value);
        }
        foreach (Enemy e in enemys)
        {
            if (enemy.id == e.id)
                e.questIDs.Add(value);
        }
    }

    //void Start()
    //{
    //    //enemys = new List<Enemy>();
    //    //if (enemyTyp.Count <= 0)
    //    //    Debug.Log("No Enemies defiend for SpawnArea");
    //    //rect = this.GetComponent<RectTransform>();

    //    //position = transform.position;
    //    //name = this.transform.name;


    //}

    public Vector3 GetSpawnPoint()
    {
        x = Random.Range(transform.position.x - (weight / 2), transform.position.x + (weight / 2 ) );
        y = Random.Range(transform.position.y - (height /2), transform.position.y + (height /2 ));
        return new Vector3(x, y, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(transform.position, new Vector3(weight, height, 0));
        rect.sizeDelta = new Vector2(weight, height);
        position = transform.position;
    }
    public void AddEnemy(Enemy enemy)
    {
        enemys.Add(enemy);
    }
    public int GetEnmyCountInArea()
    {
        return enemys.Count;
    }

}
