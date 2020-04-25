using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;

    private float bouceForce = 220;
    private int playerDir;
    public int PlayerDir
    {
        get { return playerDir; }
        set { playerDir = value; }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (playerDir)
        {
            case 1:
                attackPoint.localPosition = new Vector2(0,-4.5f);
                break;
            case 2:
                attackPoint.localPosition = new Vector2(4.5f, 0);
                break;
            case 3:
                attackPoint.localPosition = new Vector2(0, 4.5f);
                break;
            case 4:
                attackPoint.localPosition = new Vector2(4.5f, 0);
                break;
        }

        if (Input.GetKeyDown("space"))
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                // Calculate Angle Between the collision point and the player
                Vector2 dir = (Vector2)enemy.gameObject.transform.position - new Vector2(transform.position.x, transform.position.y);
                // We then get the opposite (-Vector3) and normalize it
                dir = -dir.normalized;
                // And finally we add force in the direction of dir and multiply it by force. 
                // This will push back the player
                //rb.AddForce(dir * bouceForce);

                enemy.gameObject.GetComponent<Rigidbody2D>().AddForce(-dir * (bouceForce + 450));

                //enemy.GetComponent<Rigidbody2D>().AddForce();
            }
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
        
    }
}
