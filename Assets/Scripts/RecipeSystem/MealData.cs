using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MealData : ScriptableObject
{
    
    public List<Ingredient> ingredients;

    public bool SameMeal(List<Ingredient> meal) {
        Debug.Log("comparing meals");
        if (ingredients.Count != meal.Count) {
            return false;
        }
        for (int i = 0; i < ingredients.Count; i++) {
            Debug.Log(ingredients[i]);
            if (!ingredients.Contains(meal[i]) || !meal.Contains(ingredients[i])) {
                return false;
            }
        }
        return true;
    }

}
