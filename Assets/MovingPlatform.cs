using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    

    private float speed = 1f;
    private bool direction = true;
    public Transform[] destinationPoints;
    public int initialDestination;
    public bool isLoop;
    private Transform currentDestination;
    private int indexCurrentDestination;
    private bool completeTrack;

    void Start()
    {
        if (initialDestination < 0 || initialDestination >= destinationPoints.Length)
        {
            indexCurrentDestination = 0;
        }
        else {
            indexCurrentDestination = initialDestination;
        }
        currentDestination = destinationPoints[indexCurrentDestination];
        completeTrack = false;
    }

    void Update() 
    {

        float lenght = (currentDestination.position - transform.position).magnitude;
        if (lenght < 0.05 && !completeTrack)
        {
            if (indexCurrentDestination == destinationPoints.Length - 1)
            {
                if (isLoop)
                {
                    indexCurrentDestination = 0;
                }
                else
                {
                    completeTrack = true;
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
            }
            else {
                indexCurrentDestination++;
            }
            currentDestination = destinationPoints[indexCurrentDestination];
        }

        if (!completeTrack)
        {
            Vector3 moveDirection = (currentDestination.position - transform.position).normalized;
            MoveToDirection(moveDirection);
        }



        /*else {

        }
        if (right)
        {
            float distance = (pointA - transform.position).magnitude;
            if(distance)
        }
        if (transform.position < pointA && right)
        {
            MoveToDirection(Vector2.right);
        }
        else if (transform.position.x > pointA && right == false)
        {
            MoveToDirection(Vector2.left);
        }
        else 
        {
            right = !right;
        }*/
    }

    //move left/right
    //private void MoveToDirection(Vector2 direction) => GetComponent<Rigidbody2D>().velocity = direction * speed;
    private void MoveToDirection(Vector2 direction) => transform.Translate(direction * Time.deltaTime * speed, Space.World);

}

