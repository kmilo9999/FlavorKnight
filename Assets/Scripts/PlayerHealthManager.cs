using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public int health = 3;
    private PlayerHealthUI playerHealthUI;
    private GameObject player;
    // called when the player dies. should reset the level once the level system is implemented.

    private void Start()
    {
        playerHealthUI = GameObject.Find("PlayerHealthUI").GetComponent<PlayerHealthUI>();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void KillPlayer() {
        //Camera.main.GetComponent<CameraFollow>().enabled = false;

        // make sure to set the health to zero
        this.health = 0;
        gameObject.GetComponent<NewPlayer_Movement>().Alive = false;
        //this.gameObject.SetActive(false);

    }

    public void DealDamage(int damage) {
        this.health -= damage;
   
        if (this.health <= 0) {
            KillPlayer();
        }
    }

    public void Update()
    {
        if (playerHealthUI)
        {
            playerHealthUI.modifyHealth(this.health);
        }
    }



}
