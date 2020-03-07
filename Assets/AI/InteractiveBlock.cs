using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBlock : MonoBehaviour
{
    // Start is called before the first frame update

    BoxCollider2D myCollider;
    Rigidbody2D myRigidBody;

    bool collisionPlayer = false;

    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        //myRigidBody.bodyType = RigidbodyType2D.Static;
        myRigidBody.gravityScale = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        /*if (col.gameObject.tag == "Player")
        {
            bool playerInteracting = col.gameObject.GetComponent<PlayerMovement>().getInteract();
            if (playerInteracting)
            {
                myRigidBody.bodyType = RigidbodyType2D.Dynamic;
            }
            
           
            collisionPlayer = true;
        }*/
        

        //Debug.Log("collision with: " + col.gameObject.tag);
    }

    void OnCollisionExit2D(Collision2D col)
    {
      /* if (col.gameObject.tag == "Player")
        {
            myRigidBody.bodyType = RigidbodyType2D.Static;
        }*/
    }

    void OnCollisionStay2D(Collision2D other)
    {
        //Vector2 force = new Vector2(-Vector2.forward * 1.30F);
        //other.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //other.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
        //Debug.Log("STAY " + other.gameObject.tag);
      /*  if (other != null)
        {
            PlayerMovement playerMovementComponent = other.gameObject.GetComponent<PlayerMovement>();
            if (playerMovementComponent != null)
            {
                bool playerInteracting = other.gameObject.GetComponent<PlayerMovement>().getInteract();
                if (playerInteracting)
                {
                    myRigidBody.bodyType = RigidbodyType2D.Dynamic;
                    myRigidBody.AddForce(-Vector2.right * 100.30F);
                }
            }
            
        }*/
        

    }
}
