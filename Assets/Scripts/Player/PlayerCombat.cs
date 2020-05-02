using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform attackPoint;
    public float attackRange;
    public float attackOffset = 4.5f;
    public LayerMask enemyLayer;
    public LayerMask ingredientLayer;

    private float bouceForce = 220;

    private bool attacking;

    private NewPlayer_Movement playerController;

    public LevelManager levelManager;

    void Start() {
        playerController = GetComponent<NewPlayer_Movement>();
    }

    void Update()
    {
        if (attacking)
        {
            Debug.Log("attacking");
            // todo: add food preparation
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            Debug.LogFormat("{0} objects hit", hitEnemies.Length);
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.LogFormat("Enemy: {0}", enemy.tag);
                // Calculate Angle Between the collision point and the player
                Vector2 dir = (Vector2)enemy.gameObject.transform.position - new Vector2(transform.position.x, transform.position.y);
                // We then get the opposite (-Vector3) and normalize it
                dir = -dir.normalized;
                // And finally we add force in the direction of dir and multiply it by force. 

                enemy.GetComponent<Rigidbody2D>().AddForce(-dir * (bouceForce+ 250));

                enemy.GetComponent<EnemyHealthManager>().DealDamage(1);
            }
            attacking = hitEnemies.Length == 0;
            Collider2D[] hitIngreds = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, ingredientLayer);
            foreach (Collider2D ingred in hitIngreds) {
                IngredientData ingredientData = levelManager.GetIngredientData(ingred.GetComponent<IngredientObject>().ingredient.id);
                if (ingredientData.preperable) {
                    ingred.GetComponent<IngredientObject>().ingredient.prepared = true;
                    ingred.GetComponent<SpriteRenderer>().sprite = ingredientData.preparedSprite;
                }
            }
        }
        
    }

    public void StartAttack(Direction direction) {
        attackPoint.localPosition = playerController.GetVectorDirection(direction) * attackOffset;
        attacking = true;
    }

    public void EndAttack() {
        attacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
        
    }
}
