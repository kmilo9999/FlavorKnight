using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicChestBehavior : MonoBehaviour
{
    enum MimiChest_State {
           STATIC,
           WAITING,
           RUNNING
    }

    public Animator animator;
    public float stopDistance;

    private Transform playerTransform;
    private bool closeToChest;
    private MimiChest_State currentState;
    private float waitingTimer;
    Seeker seeker;
    Path path;
    private int currentWayPoint = 0;
    private bool search;
    Rigidbody2D rb;
    private float speed = 50;
    private float nextWayPointDistance = 0.25f;

    private Vector3 leftScale;
    private Vector3 rightScale;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        currentState = MimiChest_State.STATIC;
        waitingTimer = 0;
        closeToChest = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        leftScale = transform.localScale;
        rightScale = Vector3.Scale(leftScale, new Vector3(-1f, 1f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == MimiChest_State.STATIC)
        {
            waitingTimer = 0;
            if (Input.GetKeyUp("space") && closeToChest)
            {
                search = false;
                animator.SetBool("seesPlayer", true);
                currentState = MimiChest_State.WAITING;
            }
        }
        else if (currentState == MimiChest_State.WAITING)
        {
            waitingTimer += Time.deltaTime;
            if (waitingTimer >= 2.0f)
            {
                if (Vector3.Distance(transform.position, playerTransform.position) < 3.0f)
                {
                    search = true;
                    seeker.StartPath(transform.position, playerTransform.position, OnPathComplete);
                    currentState = MimiChest_State.RUNNING;
                    animator.SetBool("startRunning", true);
                }
            }
        }
        else if (currentState == MimiChest_State.RUNNING)
        {
            if (!search)
            {
                if (currentWayPoint >= path.vectorPath.Count)
                {
                    return;
                }
                if (rb.bodyType == RigidbodyType2D.Static)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
                Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
                Vector2 force = direction * speed * Time.deltaTime;

                rb.AddForce(force);

                float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
                if (distance < nextWayPointDistance)
                {
                    currentWayPoint++;
                }

                if (rb.velocity.x >= 0.01f)
                {
                    transform.localScale = rightScale;
                }
                else if (rb.velocity.x <= -0.01f)
                {
                    transform.localScale = leftScale;
                }
            }
        }
      
        
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
            search = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            closeToChest = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            closeToChest = false;
        }
    }
}
