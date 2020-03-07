using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float speed = 1f;
    private bool right = true;
    private double leftbounds = -.5; //left bound for platform
    private double rightbound = 3.5; //right bound for platform

    private void FixedUpdate() 
    {
        if (transform.position.x < rightbound && right)
        {
            MoveToDirection(Vector2.right);
        }
        else if (transform.position.x > leftbounds && right == false)
        {
            MoveToDirection(Vector2.left);
        }
        else 
        {
            right = !right;
        }
    }

    //move left/right
    private void MoveToDirection(Vector2 direction) => GetComponent<Rigidbody2D>().velocity = direction * speed;

}

