using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealObject : MonoBehaviour
{
    
    public List<IngredientObject> ingredients;
    [SerializeField] private LevelManager lvlManager;

    public void Add(IngredientObject ingredient) {
        ingredients.Add(ingredient);
        List<Ingredient> ingredientList = new List<Ingredient>();
        foreach (IngredientObject obj in ingredients) {
            ingredientList.Add(obj.ingredient);
        }
        Debug.Log(ingredientList);
        lvlManager.CheckComplete(ingredientList);
    }

    public IngredientObject Remove() {
        IngredientObject toRemove = ingredients[ingredients.Count - 1];
        ingredients.RemoveAt(ingredients.Count - 1);
        return toRemove;
    }

    void OnCollisionStay2D(Collision2D collider) {
        if (collider.gameObject.tag == "ingredient" && collider.transform.parent == null) {
            Debug.Log("entering meal");
            Add(collider.gameObject.GetComponent<IngredientObject>());
            collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collider.gameObject.SetActive(false);
        }

    }

}
