using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer_Movement : MonoBehaviour
{

    public Animator animator;
    private Rigidbody2D rb2d;
    private float horizontalMov;
    private float verticalMov;
    private Vector3 rightScale;
    private Vector3 inverseScale;

    public float rayDistance;
    public LayerMask boxMask;

    float speed = 2.5f;
    GameObject obj;

    private bool alive;
    public bool Alive {
        get { return alive; }
        set { alive = value; }
    }

    private bool pushDragAction;
    private bool interactAction;

    private PlayerCombat pCombat;
    private PlayerItemManager pItem;

    public LevelManager level;

    private GameObject grabbedTile;
    public float grabDistance;
    public float grabOffset;


    Direction direction = Direction.up;
    PlayerState state = PlayerState.standing;

    // Scriptable object that saves a map of state and direction -> animation clip name
    public StateAnimationData animations;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        alive = true;
        pushDragAction = false;
        interactAction = false;
        pCombat = GetComponent<PlayerCombat>();
        pItem = GetComponent<PlayerItemManager>();
    }

    private float nextStateTime;

    public KeyCode interactKey;
    public KeyCode attackKey;

    private bool timerSet = false;

    public float attackTime;
    public float deathTime;

    public float pushSpeed;
    public float pushTime;
    private Vector2 pullVector;

    public struct InputCommand {
        public Direction direction;
        public bool move;
        public bool attack;
        public bool interact;
    }

    private void FixedUpdate() {
        InputCommand inp = TakeInput();
        switch (state) {
            case PlayerState.moving:
            case PlayerState.moveCarrying:
                if (inp.move) {
                    rb2d.velocity = GetVectorDirection(inp.direction) * speed;
                    if (direction != inp.direction) {
                        direction = inp.direction;
                        animator.Play(animations.GetName(state, direction));
                    }
                } else {
                    rb2d.velocity = Vector2.zero;
                }
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        InputCommand inp = TakeInput();
        // Debug.LogFormat("State: {0}, Direction: {1}; Input: Moving: {2}, Direction: {3}", state, direction, inp.move, inp.direction);
        PlayerState newState = state;
        Direction oldDirection = direction;
        // Note: this if statement is a hack necessary due to the messiness of PlayerItemManager
        // If I find some extra time I'll make this better, but for now this script needs to
        // monitor if we pick up an item and use a frame to switch to the carrying state if so.
        if (pItem.currentItem != null && state != PlayerState.standCarrying 
            && state != PlayerState.moveCarrying) {
                newState = PlayerState.standCarrying;
            }
        else if (state != PlayerState.dead && !alive) {
            newState = PlayerState.dead;
        } else {
        switch (state) {
            case PlayerState.standing:
                if (inp.attack) {
                    rb2d.velocity = Vector2.zero;
                    newState = PlayerState.attacking;
                }
                else if (inp.interact) {
                    if (Grab()) {
                        newState = PlayerState.grabbing;
                    }
                    else {
                        pItem.Pickup();
                    }
                }
                else if (inp.move) {
                    rb2d.velocity = GetVectorDirection(inp.direction) * speed;
                    direction = inp.direction;
                    newState = PlayerState.moving;
                }
                break;
            case PlayerState.moving:
                if (inp.attack) {
                    rb2d.velocity = Vector2.zero;
                    newState = PlayerState.attacking;
                }
                else if (inp.interact) {
                    if (Grab()) {
                        rb2d.velocity = Vector2.zero;
                        newState = PlayerState.grabbing;
                    } else {
                        pItem.Pickup();
                    }
                }
                else if (!inp.move) {
                    rb2d.velocity = Vector2.zero;
                    newState = PlayerState.standing;
                } else {
                    rb2d.velocity = GetVectorDirection(inp.direction) * speed;
                    direction = inp.direction;
                }
                break;
            case PlayerState.standCarrying:
                if (inp.interact) {
                    pItem.Drop();
                    newState = PlayerState.standing;
                }
                else if (inp.move) {
                    rb2d.velocity = GetVectorDirection(inp.direction) * speed;
                    direction = inp.direction;
                    newState = PlayerState.moveCarrying;
                }
                break;
            case PlayerState.moveCarrying:
                if (inp.interact) {
                    pItem.Drop();
                    rb2d.velocity = Vector2.zero;
                    newState = PlayerState.standing;
                }
                else if (!inp.move) {
                    rb2d.velocity = Vector2.zero;
                    newState = PlayerState.standCarrying;
                } else {
                    rb2d.velocity = GetVectorDirection(inp.direction) * speed;
                    direction = inp.direction;
                }
                break;
            case PlayerState.attacking:
                // todo: improve feel of combat
                if (!timerSet) {
                    nextStateTime = attackTime;
                    timerSet = true;
                    pCombat.StartAttack(direction);
                } else {
                    nextStateTime -= Time.deltaTime;
                }
                if (nextStateTime < 0) {
                    timerSet = false;
                    pCombat.EndAttack();
                    newState = PlayerState.standing;
                }
                // should you be able to turn while attacking?
                // direction = inp.direction;
                break;
            case PlayerState.grabbing:
                if (inp.move && 
                    (GetVectorDirection(inp.direction).Equals(GetVectorDirection(direction)) || 
                    GetVectorDirection(inp.direction).Equals(-1 * GetVectorDirection(direction)))) {
                        pullVector = GetVectorDirection(inp.direction) * pushSpeed;
                        rb2d.velocity = pullVector;
                        grabbedTile.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                        grabbedTile.GetComponent<Rigidbody2D>().velocity = pullVector;
                        newState = PlayerState.pushing;
                    } 
                else if (inp.interact) {
                    grabbedTile = null;
                    newState = PlayerState.standing;
                }
                break;
            case PlayerState.pushing:
                if (!timerSet) {
                    nextStateTime = pushTime; // todo: calculate based on grid distance
                    timerSet = true;
                } else {
                    nextStateTime -= Time.deltaTime;
                }
                if (nextStateTime < 0) {
                    timerSet = false;
                    rb2d.velocity = Vector2.zero;
                    grabbedTile.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    grabbedTile.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    newState = PlayerState.grabbing;
                }
                break;
            case PlayerState.dead:
                rb2d.velocity = Vector2.zero;
                if (!timerSet) {
                    nextStateTime = deathTime;
                    timerSet = true;
                } else {
                    nextStateTime -= Time.deltaTime;
                }
                rb2d.isKinematic = true;
                if (nextStateTime < 0) {
                    timerSet = false;
                    level.EndGame();
                }
                break;
        }
            }
        // todo fix animations
        if (direction != oldDirection || state != newState) {
            animator.Play(animations.GetName(newState, direction));
        }
        state = newState;
    }

    private InputCommand TakeInput()
    {
        InputCommand toReturn =  new InputCommand();
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");
        toReturn.move = Mathf.Abs(vInput) > 0 || Mathf.Abs(hInput) > 0;
        if (Mathf.Abs(vInput) > Mathf.Abs(hInput)) {
            if (vInput > 0) {
                toReturn.direction = Direction.up;
            } else {
                toReturn.direction = Direction.down;
            }
        } else {
            if (hInput > 0) {
                toReturn.direction = Direction.right;
            } else {
                toReturn.direction = Direction.left;
            }
        }
        toReturn.interact = Input.GetKeyDown(interactKey);
        toReturn.attack = Input.GetKeyDown(attackKey);
        return toReturn;
    }

    private bool Grab() {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, GetVectorDirection(), grabDistance);
        Debug.DrawRay(transform.position, GetVectorDirection() * grabDistance, Color.red, 1);
        foreach (RaycastHit2D hit in hits) {
            Debug.LogFormat("Checking hit; Hit: {0}", hit.collider);
            if (hit.collider != null && 
                ((hit.collider.tag == "pot" && hit.collider.GetComponent<CookingPot>().ingredient == null)
                 || hit.collider.tag == "grabable")) {
                Debug.Log("Hit grabbable tile");
                transform.position = hit.transform.position + (Vector3)(-1 * GetVectorDirection() * grabOffset);
                grabbedTile = hit.collider.gameObject;
                return true;
            }
        }
        return false;
     }

    public Vector2 GetVectorDirection(Direction direction) {
        switch (direction) {
            case Direction.left:
                return new Vector2(-1, 0);
            case Direction.up:
                return new Vector2(0, 1);
            case Direction.right:
                return new Vector2(1, 0);
            case Direction.down:
                return new Vector2(0, -1);
        }
        Debug.Log("WARNING: switch did not find direction");
        return Vector2.zero;
    }

    public Vector2 GetVectorDirection() {
        return GetVectorDirection(this.direction);
    }

}
