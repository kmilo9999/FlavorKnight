using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public LevelData levelData; 
    public IngredientData[] ingredientDatas;

    public void CheckComplete(List<Ingredient> meal) {
        if (levelData.mealBlueprint.SameMeal(meal)) {
            Debug.Log("LEVEL COMPLETE"); // todo
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }

    public IngredientData GetIngredientData(IngredientID id) {
        foreach (IngredientData data in ingredientDatas) {
            if (id == data.id) {
                return data;
            }
        }
        Debug.LogError("Ingredient data not found for id: " + id);
        return null;
    }

    public void EndGame() {
        Debug.Log("GAME OVER");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void WinLevel() {
        Debug.Log("LEVEL WON");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    
}
