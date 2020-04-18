using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    public GameObject currentItem;
    public KeyCode pickupKey;
    public KeyCode dropKey;

    public BoxCollider2D pickupCollider;

    void Start() {
        pickupCollider.enabled = false;
    }


    void Update() { // TODO: adding food preparing w/ check preparable
        if (Input.GetKeyDown(dropKey)) {
            if (currentItem != null) {
                currentItem.transform.parent = null;
                currentItem = null;
            }
        }
        if (Input.GetKeyDown(pickupKey)) {
            Debug.Log("Trying to pick up ingredient.");
            StartCoroutine(FlashPickupBox());
        }
    }

    private IEnumerator FlashPickupBox() {
        Debug.Log("Hit coroutine");
        pickupCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        pickupCollider.enabled = false;
    }  

    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Encountered collider");
        if (collider.tag == "ingredient" && currentItem == null) {
            currentItem = collider.gameObject;
            collider.transform.parent = transform;
            Debug.Log("changed parent:");
            Debug.Log(collider.transform.parent);
        }
        if (collider.tag == "pot" && currentItem == null && collider.GetComponent<CookingPot>().ingredient != null) {
            Debug.Log("removing item from log");
            IngredientObject ingredient = collider.GetComponent<CookingPot>().ingredient;
            collider.GetComponent<CookingPot>().ingredient = null;
            ingredient.gameObject.SetActive(true);
            ingredient.transform.parent = transform;
            currentItem = ingredient.gameObject;
        }
        Debug.Log(collider.tag);
        if (collider.tag == "plate" && currentItem == null) {
            Debug.Log("removing item from plate");
            IngredientObject ingredient = collider.GetComponent<MealObject>().Remove();
            ingredient.gameObject.SetActive(true);
            ingredient.transform.parent = transform;
            currentItem = ingredient.gameObject;
        }
    }
    
}
