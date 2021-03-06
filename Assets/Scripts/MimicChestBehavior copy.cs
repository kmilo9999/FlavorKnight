﻿/* using Pathfinding;
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
    private float speed = 250;
    private float nextWayPointDistance = 0.25f;

    private Vector3 leftScale;
    private Vector3 rightScale;

    private float bouceForce = 220;

    public AudioSource openSound;
    public AudioSource swingSound;

    public KeyCode interactKey;
    

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
            if (Input.GetKeyDown(interactKey) && closeToChest)
            {
                search = false;
                animator.SetBool("seesPlayer", true);
                currentState = MimiChest_State.WAITING;
                openSound.Play();

                Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
                if (directionToPlayer.x >= 0.01f)
                {
                    transform.localScale = rightScale;
                }
                else if (directionToPlayer.x <= -0.01f)
                {
                    transform.localScale = leftScale;
                }

            }
        }
        else if (currentState == MimiChest_State.WAITING)
        {
            waitingTimer += Time.deltaTime;
            if (waitingTimer >= 2.0f)
            {
             
               search = true;
               gameObject.tag = "enemy";
               gameObject.layer = 14;
               InvokeRepeating("UpdatePath", 0f, 0.5f);
               currentState = MimiChest_State.RUNNING;
               animator.SetBool("startRunning", true);
             
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
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    rb.drag = 1.5f;
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

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, playerTransform.position, OnPathComplete);
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
        if (currentState == MimiChest_State.STATIC && collider.gameObject.tag == "Player")
        {
            closeToChest = true;
        }

        if (currentState == MimiChest_State.RUNNING && collider.gameObject.tag == "Player")
        {
            swingSound.Play();
            // Calculate Angle Between the collision point and the player
            Vector2 dir = collider.GetContact(0).point - new Vector2(transform.position.x, transform.position.y);
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            
            if (collider.gameObject.GetComponent<NewPlayer_Movement>().Alive)
            {
                rb.AddForce(dir * bouceForce);
            }
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(-dir * (bouceForce));
            collider.gameObject.GetComponent<PlayerHealthManager>().DealDamage(1);
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
*/