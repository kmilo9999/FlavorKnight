using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public int health = 3;

    // called when the player dies. should reset the level once the level system is implemented.
    public void KillPlayer() {
        Camera.main.GetComponent<CameraFollow>().enabled = false;
        this.gameObject.SetActive(false);
    }

    public void DealDamage(int damage) {
        this.health -= damage;
        if (this.health <= 0) {
            KillPlayer();
        }
    }



}
