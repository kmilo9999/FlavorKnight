using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrapTile : MonoBehaviour
{

    

    private void OnTriggerEnter2D(Collider2D collider) {

        if (GetComponent<BoolStateTileScript>().BoolValue)
        {
            if (collider.gameObject.tag == "Player")
            {
                Debug.Log("killing the player");
                collider.gameObject.GetComponent<PlayerHealthManager>().KillPlayer();
            }
        }
       
    }


}
