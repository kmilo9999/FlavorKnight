using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrapTile : MonoBehaviour
{

    public PlayerHealthManager playerHealth;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            Debug.Log("killing the player");
            playerHealth.KillPlayer();
        }
    }


}
