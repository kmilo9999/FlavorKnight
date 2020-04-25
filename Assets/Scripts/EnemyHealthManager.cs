using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int health;
    // called when the player dies. should reset the level once the level system is implemented.

    private void Start()
    {
        
    }


    public void KillEnemy()
    {
        //Camera.main.GetComponent<CameraFollow>().enabled = false;

        // make sure to set the health to zero
        this.health = 0;
        //gameObject.GetComponent<NewPlayer_Movement>().Alive = false;
        Destroy(gameObject);
        //this.gameObject.SetActive(false);

    }

    public void DealDamage(int damage)
    {
        this.health -= damage;

        if (this.health <= 0)
        {
            KillEnemy();
        }
    }

    public void Update()
    {
        
    }
}
