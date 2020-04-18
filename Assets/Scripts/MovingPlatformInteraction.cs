using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformInteraction : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player.transform.parent = transform;
        }
        else if (collider.gameObject.tag == "grabable")
        {
            collider.gameObject.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player.transform.parent =  null;
        }
        else if (collider.gameObject.tag == "grabable")
        {
            collider.gameObject.transform.parent = null;
        }
    }
    
       
    
}
