using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float offset = 10;

    private void LateUpdate()
    {
        if (transform.position.y < player.position.y - offset)
        {
            transform.position = new Vector3(transform.position.x, player.position.y - offset, transform.position.z);
        } 
        else if (transform.position.y > player.position.y + offset)
        {
            transform.position = new Vector3(transform.position.x, player.position.y + offset, transform.position.z);
        }

        if (transform.position.x < player.position.x - offset)
        {
            transform.position = new Vector3(player.position.x - offset, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > player.position.x + offset)
        {
            transform.position = new Vector3(player.position.x + offset, transform.position.y, transform.position.z);
        }
    }
}
