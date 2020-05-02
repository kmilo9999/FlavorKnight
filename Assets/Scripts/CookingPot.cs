using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingPot : MonoBehaviour
{

    public IngredientObject ingredient;
    private float cookingTime = 0;
    public LevelManager levelManager;
    public Vector2 kickOutVector = new Vector2(5, 0);
    public bool boiling = false;

    private Animator animator;

    public AudioSource boilingSound;
    public AudioSource cookedSound;
    public AudioSource burnedSound;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (boiling && ingredient != null) {
            if (!boilingSound.isPlaying) {
                if (cookingTime == 0) {
                    Debug.Log("Play Sound");
                    boilingSound.Stop();
                    boilingSound.Play();
                } else {
                    Debug.Log("Unpause sound");
                    boilingSound.UnPause();
                }
            }
        } else {
            boilingSound.Pause();
        }
        animator.SetBool("boiling", boiling && ingredient != null);
        if (ingredient != null && boiling) {
            // TODO: change to do burning and stuff + check cookable
            cookingTime += Time.deltaTime;
            IngredientData data = levelManager.GetIngredientData(ingredient.ingredient.id);
            if (data.id == ingredient.ingredient.id && cookingTime > data.burnTime) {
                burnedSound.Play();
                levelManager.HandleBurned(ingredient.ingredient.id);
                GameObject.Destroy(ingredient.gameObject);
                ingredient = null;
                Debug.Log("Ingredient Burned! Destroying...");
            }
            if (ingredient && data.id == ingredient.ingredient.id && cookingTime > data.cookTime) {
                    if (!ingredient.ingredient.cooked) {
                        cookedSound.Play();
                    }
                    ingredient.ingredient.cooked = true;
                    ingredient.GetComponent<SpriteRenderer>().sprite = data.cookedSprite;
            }
        }
    }

    void OnCollisionStay2D(Collision2D collider) {
        Debug.Log("Pot collision activated.");
        if (collider.gameObject.tag == "ingredient" && collider.transform.parent == null){
            Ingredient thatIngredient = collider.gameObject.GetComponent<IngredientObject>().ingredient;
            IngredientData data = levelManager.GetIngredientData(thatIngredient.id);
            if (this.ingredient == null && (!data.preperable || thatIngredient.prepared)) {
                Debug.Log("entering pot");
                Debug.Log(collider.transform.parent);
                this.ingredient = collider.gameObject.GetComponent<IngredientObject>();
                collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                collider.gameObject.SetActive(false);
                cookingTime = 0;
            } else {
                // Todo: reject more intelligently
                Debug.Log("rejecting from pot");
                collider.transform.position = (Vector2) this.transform.position + kickOutVector;
            }
        }
    }

}
