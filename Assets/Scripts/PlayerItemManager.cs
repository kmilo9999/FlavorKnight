using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{

    public float dropDistance = 1;
    public GameObject currentItem;
    public KeyCode pickupKey;
    public KeyCode dropKey;
    public KeyCode prepareKey;

    public BoxCollider2D pickupCollider;

    private PlayerMovement movementController;
    private float pickupColliderOffset;

    void Start() {
        pickupCollider.enabled = false;
        movementController = GetComponent<PlayerMovement>();
        pickupColliderOffset = pickupCollider.offset.x;
    }

    void Update() { // TODO: adding food preparing w/ check preparable
        if (Input.GetKeyDown(dropKey)) {
            if (currentItem != null) {
                currentItem.transform.localPosition = movementController.GetVectorDirection() * dropDistance;
                currentItem.transform.parent = null;
                PlayerMovement.Direction direction = movementController.GetDirection();
                currentItem.GetComponent<BoxCollider2D>().enabled = true;
                movementController.StopCarrying();
                currentItem = null;
            }
        }
        if (Input.GetKeyDown(pickupKey)) {
            Debug.Log("Trying to pick up ingredient.");
            StartCoroutine(FlashPickupBox());
        }
    }

    private IEnumerator FlashPickupBox() {
        pickupCollider.offset = pickupColliderOffset * movementController.GetVectorDirection();
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
            collider.transform.localPosition = new Vector2(0, 7f);
            collider.GetComponent<BoxCollider2D>().enabled = false;
            movementController.StartCarrying();
            Debug.Log("changed parent:");
            Debug.Log(collider.transform.parent);
        }
        if (collider.tag == "pot" && currentItem == null && collider.GetComponent<CookingPot>().ingredient != null) {
            Debug.Log("removing item from log");
            IngredientObject ingredient = collider.GetComponent<CookingPot>().ingredient;
            collider.GetComponent<CookingPot>().ingredient = null;
            ingredient.gameObject.SetActive(true);
            ingredient.transform.parent = transform;
            ingredient.transform.localPosition = new Vector2(0, 7f);
            movementController.StartCarrying();
            currentItem = ingredient.gameObject;
        }
        Debug.Log(collider.tag);
        if (collider.tag == "plate" && currentItem == null) {
            Debug.Log("removing item from plate");
            IngredientObject ingredient = collider.GetComponent<MealObject>().Remove();
            ingredient.gameObject.SetActive(true);
            ingredient.transform.parent = transform;
            ingredient.transform.localPosition = new Vector2(0, 7f);
            movementController.StartCarrying();
            currentItem = ingredient.gameObject;
        }
    }
    
}
