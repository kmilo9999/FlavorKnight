using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingPot : MonoBehaviour
{

    public IngredientObject ingredient;
    private float cookingTime = 0;
    public LevelManager levelManager;

    void Update() {
        if (ingredient != null) {
            // TODO: change to do burning and stuff + check cookable
            cookingTime += Time.deltaTime;
            IngredientData data = levelManager.GetIngredientData(ingredient.ingredient.id);
            if (data.id == ingredient.ingredient.id && cookingTime > data.cookTime) {
                    ingredient.ingredient.cooked = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.tag == "ingredient" && collider.transform.parent == null && ingredient == null) {
            Debug.Log("entering pot");
            Debug.Log(collider.transform.parent);
            ingredient = collider.GetComponent<IngredientObject>();
            collider.gameObject.SetActive(false);
            cookingTime = 0;
        }
    }

}
