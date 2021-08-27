using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public GameObject GFX_Sprite;

    public float lookRadius = 10f;
    public float stopDistance = 0.5f;
    Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    EnemyCombat combat;
    Enemy enemy;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    public bool DrawCollisionArea = true;
    void Start()
    {
        enemy = this.GetComponent<Enemy>();
        target = PlayerManager.instance.player.transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        combat = GetComponent<EnemyCombat>();

        stopDistance += enemy.radius;
        InvokeRepeating("UpdatePath", 0f, .5f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector2.Distance(target.position, transform.position);
        float saveme = distance;
        if (distance <= lookRadius && distance > stopDistance)
        {
            if (path == null)
                return;
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else if (currentWaypoint < path.vectorPath.Count)
                reachedEndOfPath = false;

            if (!reachedEndOfPath)
            {
                Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                Vector2 force = direction * speed * Time.deltaTime;

                rb.AddForce(force);

                distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
                if (distance < nextWaypointDistance)
                    currentWaypoint++;

                if (rb.velocity.x >= 0.01f)
                   GFX_Sprite.transform.eulerAngles = new Vector3(0, 0, 0);
                else if (rb.velocity.x <= -0.01f)
                    GFX_Sprite.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else if(distance <= stopDistance)
        {
            CharackterStats targetStats = target.GetComponent<CharackterStats>();
            if(targetStats != null)
            {
                combat.Attack(targetStats);
            }
        }

    }

    void UpdatePath()
    {

        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void OnDrawGizmosSelected()
    {
        if(DrawCollisionArea)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, lookRadius);
        }
    }
}
