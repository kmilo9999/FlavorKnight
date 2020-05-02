using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int health;
    // called when the player dies. should reset the level once the level system is implemented.

    public GameObject ingredient;

    void Start() {
        ingredient.SetActive(false);
    }

    public void KillEnemy()
    {
        //Camera.main.GetComponent<CameraFollow>().enabled = false;

        // make sure to set the health to zero
        this.health = 0;
        //gameObject.GetComponent<NewPlayer_Movement>().Alive = false;
        DropIngredient();
        Destroy(gameObject);
        //this.gameObject.SetActive(false);
    }

    public void DealDamage(int damage)
    {
        Debug.Log("Hit enemy");
        this.health -= damage;

        if (this.health <= 0)
        {
            KillEnemy();
        } else {
            Debug.Log("Enemy flashing");
            StartCoroutine(FlashColor());
        }
    }

    IEnumerator FlashColor() {
        Color prevColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.5f);
        GetComponent<SpriteRenderer>().color = prevColor;
    }

    private void DropIngredient() {
        ingredient.SetActive(true);
        ingredient.transform.parent = null;
    }

}
