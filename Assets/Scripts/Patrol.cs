using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Patrol : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] targets;

    public float speed;
    public float nextWayPointDistance;

    Path path;
    int currentWayPoint = 0;
    bool reachEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    private int currentTarget;
    private Vector3[] points;
    private bool search;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        currentTarget = 0;

        //update path to first position
        UpdatePath();
        search = false;
        //InvokeRepeating("UpdatePath", 0f, 0.5f);

        // Transform to Vector3 array
        points = new Vector3[targets.Length];

        for (int i = 0; i < 3; i++)
        {
            points[i] = targets[i].position;
        }

    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, targets[currentTarget].position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (targets.Length == 0 || path == null) return;

        /* if (path == null)
         {
             return;
         }*/


        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachEndOfPath = true;
            currentTarget++;
            currentTarget = currentTarget % targets.Length;
            path = null;
            UpdatePath();

            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }




    }
}
