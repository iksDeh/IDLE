using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;



public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float aggroRange = 5;
    public float stopDistance = 0.5f;

    private Vector2 vectorAggroRage;
    private Vector2 vectorstopDistance;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        vectorAggroRage = new Vector2(aggroRange, aggroRange);
        vectorstopDistance = new Vector2(stopDistance, stopDistance);

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();


        InvokeRepeating("UpdatePath", 0f, .5f);

    }

    void UpdatePath()
    {

            if (seeker.IsDone())
                seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
            if(!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
    }

    private bool IsInAggroRange()
    {
        Vector2 distance = (Vector2)target.position - rb.position;
        Vector2 negativeRange = (vectorAggroRage) * -1;
        Vector2 negativeStopDis = (vectorstopDistance) * -1;

        if (distance.x >= 0 && distance.x >= stopDistance || distance.x < 0 && distance.x <= (stopDistance * -1))
            if (distance.y >= 0 && distance.y >= stopDistance || distance.y < 0 && distance.y <= (stopDistance * -1))
                if (distance.x <= aggroRange && distance.y <= aggroRange || distance.x <= aggroRange && distance.y <= (aggroRange * -1))
                    return true;
                else if (distance.x <= (aggroRange * -1) && distance.y <= aggroRange || distance.x <= (aggroRange * -1) && distance.y <= (aggroRange * -1))
                    return true;

        return false;
    }

    void FixedUpdate()
    {
        if (path == null)
            return;
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else if(currentWaypoint < path.vectorPath.Count)
        {
            reachedEndOfPath = false;
        }

       // if(IsInAggroRange())
           if (!reachedEndOfPath)
            {
                Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                Vector2 force = direction * speed * Time.deltaTime;

                rb.AddForce(force);

                float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

                if (distance < nextWaypointDistance)
                    currentWaypoint++;
            }

    }
}

