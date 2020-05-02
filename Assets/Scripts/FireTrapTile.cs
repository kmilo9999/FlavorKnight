using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrapTile : MonoBehaviour
{

    CookingPot pot = null;

    void Update() {
        Debug.LogFormat("Pot: ", pot);
        if (pot) {
            if (GetComponent<BoolStateTileScript>().BoolValue) {
                pot.boiling = true;
            } else {
                pot.boiling = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Triggered fire");
        if (GetComponent<BoolStateTileScript>().BoolValue)
        {
            if (collider.gameObject.tag == "Player")
            {
                Debug.Log("killing the player");
                collider.gameObject.GetComponent<PlayerHealthManager>().KillPlayer();
            }
        }
        if (collider.tag == "pot") 
        {
            pot = collider.GetComponent<CookingPot>();
        }
       
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "pot" && pot && collider.gameObject == pot.gameObject) 
        {
            pot.boiling = false;
            pot = null;
        }
    }


}
