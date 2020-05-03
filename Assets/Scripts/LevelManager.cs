using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public LevelData levelData; 
    public IngredientData[] ingredientDatas;
    // Note: this is a temporary solution to the problem of losing ingredients from burning
    // The ordering of ingredient data objects maps to the corresponding enemy drops
    // When an ingredient is burned we can look in the map and respawn the corresponding enemy
    public GameObject[] enemies;

    private Dictionary<IngredientID, GameObject> lootMapping;

    public GameObject winUI;

    private bool won = false;

    public float continueTime;

    void Start() {
        winUI.SetActive(false);
        lootMapping = new Dictionary<IngredientID, GameObject>();
        for (int i = 0; i < ingredientDatas.Length; i++) {
            GameObject prototype = GameObject.Instantiate(enemies[i]);
            prototype.SetActive(false);
            lootMapping[ingredientDatas[i].id] = prototype;
        }
    }

    void Update() {
        if (won) {
            Debug.Log(continueTime);
            if (continueTime < 0 && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))) {
                Time.timeScale = 1;
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            } else {
                continueTime -= Time.deltaTime;
            }
        }
    }

    public void HandleBurned(IngredientID id) {
        GameObject newEnemy = GameObject.Instantiate(lootMapping[id]);
        newEnemy.SetActive(true);
    }

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
        winUI.SetActive(true);
        won = true;
        Time.timeScale = 0.0f;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    
}
