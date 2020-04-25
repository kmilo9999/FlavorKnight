using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer_Movement : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb2d;
    private float horizontalMov;
    private float verticalMov;
    private Vector3 rightScale;
    private Vector3 inverseScale;

    public float rayDistance;
    public LayerMask boxMask;

    float speed = 2.5f;
    GameObject obj;

    private bool alive;
    public bool Alive {
        get { return alive; }
        set { alive = value; }
    }

    private bool pushDragAction;
    private bool interactAction;

    private PlayerCombat pCombat;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rightScale = transform.localScale;
        inverseScale = Vector2.Scale(rightScale, new Vector2(-1, 1));
        alive = true;
        pushDragAction = false;
        interactAction = false;
        pCombat = GetComponent<PlayerCombat>();
    }


    private void Update()
    {
        if (alive)
        {
            movementLogic();
            pushDragLogic();
            attackLogic();
            
        }
        else {
            deathLogic();
        }

       
    }

    private void attackLogic()
    {
        if (Input.GetKeyDown("space") && !pushDragAction && !interactAction)
        {
            animator.SetBool("attack", true);
        }
    }

    private void deathLogic()
    {
        rb2d.velocity = new Vector2(0, 0);
        rb2d.isKinematic = true;
        animator.SetBool("alive", false);

    }

    private void pushDragLogic()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x,
            rayDistance, boxMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x,
            rayDistance, boxMask);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up * transform.localScale.x,
            rayDistance, boxMask);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down * transform.localScale.x,
            rayDistance, boxMask);

        bool hit = false;
        if (hitRight.collider != null)
        {
            //resultRay = hitRight;
            resolveRayHit(hitRight);
            hit = true;
        }
        if (hitLeft.collider != null)
        {
            //resultRay = hitLeft;
            resolveRayHit(hitLeft);
            hit = true;
        }
        if (hitUp.collider != null)
        {
            resolveRayHit(hitUp);
            hit = true;
        }
        if (hitDown.collider != null)
        {
            resolveRayHit(hitDown);
            hit = true;
        }

        if (!hit)
        {
            pushDragAction = false;
            interactAction = false;
        }
       
    }

    private void resolveRayHit(RaycastHit2D rayHit)
    {
        if (rayHit.collider != null && rayHit.collider.gameObject.tag == "grabable")
        {
            if (Input.GetKeyDown("space"))
            {
                obj = rayHit.collider.gameObject;
                obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                obj.GetComponent<FixedJoint2D>().enabled = true;
                obj.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
                pushDragAction = true;
            }
            else if (Input.GetKeyUp("space"))
            {
                if (obj != null && obj.GetComponent<FixedJoint2D>() != null)
                {
                    obj.GetComponent<FixedJoint2D>().enabled = false;
                    Debug.Log("SUPER HERE");
                    obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    obj = null;
                    pushDragAction = false;
                }

            }
        } else if (rayHit.collider != null && rayHit.collider.gameObject.tag == "interactable")
        {
            interactAction = true;
        }

    }

    private void movementLogic()
    {
        horizontalMov = Input.GetAxis("Horizontal");
        verticalMov = Input.GetAxis("Vertical");

        if (horizontalMov > 0)
        {
            animator.SetInteger("move_direction", 2);
            pCombat.PlayerDir = 2;
            transform.localScale = rightScale;
        }
        else if (horizontalMov < 0)
        {
            animator.SetInteger("move_direction", 2);
            pCombat.PlayerDir = 4;
            transform.localScale = inverseScale;
        }

        if (verticalMov < 0)
        {
            pCombat.PlayerDir = 1;
            animator.SetInteger("move_direction", 1);
        }
        else if (verticalMov > 0)
        {
            pCombat.PlayerDir = 3;
            animator.SetInteger("move_direction", 3);
        }

        animator.SetFloat("speed", rb2d.velocity.magnitude);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (alive)
        {
            rb2d.velocity = new Vector2(horizontalMov * speed, verticalMov * speed);
        }
        
    }

    public void attackEnds()
    {
        animator.SetBool("attack", false);
        
    }



}
