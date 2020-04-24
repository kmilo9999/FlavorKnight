using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    enum Direction { UP, DOWN, LEFT, RIGHT };

    // This controls how quickly the player moves
    public float mvAmt = 5;

    // This is the index for the walking animation
    private int animationIndex;


    // This is the sprite to use when standing still
    public Sprite standingUp;
    // This is a list of sprites to use when moving
    public List<Sprite> walkingUp;



    // This is the sprite to use when standing still
    public Sprite standingRight;
    // This is a list of sprites to use when moving
    public List<Sprite> walkingRight;


    // This is the sprite to use when standing still
    public Sprite standingLeft;
    // This is a list of sprites to use when moving
    public List<Sprite> walkingLeft;


    // This is the sprite to use when standing still
    public Sprite standingDown;
    // This is a list of sprites to use when moving
    public List<Sprite> walkingDown;


    // Sprites for the respective attack directions
    public List<Sprite> attackUp;
    public List<Sprite> attackRight;
    public List<Sprite> attackLeft;
    public List<Sprite> attackDown;

    // Sprites for walking while carrying something
    public List<Sprite> carryUp;
    public List<Sprite> carryRight;
    public List<Sprite> carryLeft;
    public List<Sprite> carryDown;

    public List<Sprite> deathAnimation;

    private bool carrying;

    public KeyCode attackKey = KeyCode.J;



    // This is the renderer of the player
    public SpriteRenderer spriteRenderer;

    // How quickly the next frame is advanced for walking, in seconds
    public float animationSpeed;


    private Direction direction;



    private float currentTime;

    private bool interact = false;



    // This keeps track of horizontal movement
    private float horz;
    // This keeps track of vertical movement
    private float vert;
    // This keeps track of whether or not the attack key is pressed
    private bool attackingInput;
    // This keeps track of whether or not we are in the middle of an attack animation
    private bool attacking;


    // This is the rigid body that controls the physics of the player
    private Rigidbody2D rb;


    public LayerMask boxMask;
    public float rayDistance;

    GameObject obj;
    Direction GetDirection() {
        return direction;
    }

    void startCarrying()
    {
        carrying = true;
    }

    void stopCarrying()
    {
        carrying = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animationIndex = 0;
        currentTime = 0;
        direction = Direction.DOWN;
        carrying = false;
    }

    // Update is called once per frame
    void Update()
    {
        horz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.J))
            attackingInput = true;
        // Temporarily making it so that you need to hold a button down
        if (Input.GetKeyDown(KeyCode.K))
            carrying = !carrying;

        if (Input.GetKeyDown("space"))
        {
            interact = true;
            
        }
        else
        {
            interact = false;
        }

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x,
            rayDistance, boxMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x,
            rayDistance, boxMask);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up * transform.localScale.x,
            rayDistance, boxMask);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down * transform.localScale.x,
            rayDistance, boxMask);

        RaycastHit2D resultRay;

        if (hitRight.collider != null)
        {
            //resultRay = hitRight;
            resolveRayHit(hitRight);
        }
         if (hitLeft.collider != null)
        {
            //resultRay = hitLeft;
            resolveRayHit(hitLeft);
        }
         if (hitUp.collider != null)
        {
            resolveRayHit(hitUp);
        }
         if (hitDown.collider != null)
        {
            resolveRayHit(hitDown);
        }
        
    }

    private void resolveRayHit(RaycastHit2D rayHit)
    {
        if (rayHit.collider != null && rayHit.collider.gameObject.tag == "grabable" && Input.GetKeyDown("space"))
        {
            obj = rayHit.collider.gameObject;
            obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            obj.GetComponent<FixedJoint2D>().enabled = true;
            obj.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
        }
        else if (Input.GetKeyUp("space"))
        {
            if (obj != null && obj.GetComponent<FixedJoint2D>() != null)
            {
                obj.GetComponent<FixedJoint2D>().enabled = false;
                Debug.Log("SUPER HERE");
                obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * rayDistance);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * transform.localScale.x * rayDistance);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.up * transform.localScale.x * rayDistance);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * transform.localScale.x * rayDistance);
    }

    // This is called for every physics update

    // This is called for a fixed time update (not by frame)
    void FixedUpdate()
    {
        if (attacking)
        {
            if (direction == Direction.UP)
            {
                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    if (animationIndex < attackUp.Count)
                    {
                        spriteRenderer.sprite = attackUp[animationIndex];
                        currentTime = currentTime - animationSpeed;
                        currentTime = currentTime + Time.deltaTime;

                    }
                    else
                    {
                        attacking = false;
                        currentTime = 0;
                        animationIndex = 0;
                    }
                }
                else
                {
                    currentTime = currentTime + Time.deltaTime;
                }

            }
            else if (direction == Direction.RIGHT)
            {
                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    if (animationIndex < attackRight.Count)
                    {
                        spriteRenderer.sprite = attackRight[animationIndex];
                        currentTime = currentTime - animationSpeed;
                        currentTime = currentTime + Time.deltaTime;
                    }
                    else
                    {
                        attacking = false;
                        currentTime = 0;
                        animationIndex = 0;
                    }
                }
                else
                {
                    currentTime = currentTime + Time.deltaTime;
                }

            }
            else if (direction == Direction.LEFT)
            {
                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    if (animationIndex < attackLeft.Count)
                    {
                        spriteRenderer.sprite = attackLeft[animationIndex];
                        currentTime = currentTime - animationSpeed;
                        currentTime = currentTime + Time.deltaTime;
                    }
                    else
                    {
                        attacking = false;
                        currentTime = 0;
                        animationIndex = 0;
                    }
                }
                else
                {
                    currentTime = currentTime + Time.deltaTime;
                }
            }
            else
            {
                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    if (animationIndex < attackDown.Count)
                    {
                        spriteRenderer.sprite = attackDown[animationIndex];
                        currentTime = currentTime - animationSpeed;
                        currentTime = currentTime + Time.deltaTime;
                    }
                    else
                    {
                        attacking = false;
                        currentTime = 0;
                        animationIndex = 0;
                    }
                }
                else
                {
                    currentTime = currentTime + Time.deltaTime;
                }
            }
        }
        else if (carrying)
        {
            rb.velocity = new Vector2(horz, vert).normalized;
            float speedFactor = Time.deltaTime * mvAmt;
            rb.velocity = Vector2.Scale(rb.velocity, new Vector2(speedFactor, speedFactor));
            if (vert < -0.0001)
            {
                if (direction != Direction.DOWN)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    direction = Direction.DOWN;
                }

                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    animationIndex = animationIndex % carryDown.Count;
                    currentTime = currentTime - animationSpeed;
                }
                spriteRenderer.sprite = carryDown[animationIndex];
                currentTime = currentTime + Time.deltaTime;


            }
            else if (vert > 0.0001)
            {
                if (direction != Direction.UP)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    direction = Direction.UP;
                }

                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    animationIndex = animationIndex % carryUp.Count;
                    currentTime = currentTime - animationSpeed;
                }
                spriteRenderer.sprite = carryUp[animationIndex];
                currentTime = currentTime + Time.deltaTime;
                direction = Direction.UP;
            }
            else if (horz < -0.0001)
            {
                if (direction != Direction.LEFT)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    direction = Direction.LEFT;
                }

                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    animationIndex = animationIndex % carryLeft.Count;
                    currentTime = currentTime - animationSpeed;
                }
                spriteRenderer.sprite = carryLeft[animationIndex];
                currentTime = currentTime + Time.deltaTime;
                direction = Direction.LEFT;
            }
            else if (horz > 0.0001)
            {
                if (direction != Direction.RIGHT)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    direction = Direction.RIGHT;
                }

                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    animationIndex = animationIndex % carryRight.Count;
                    currentTime = currentTime - animationSpeed;
                }
                spriteRenderer.sprite = carryRight[animationIndex];
                currentTime = currentTime + Time.deltaTime;
                direction = Direction.RIGHT;
            }

            else
            {
                currentTime = 0;
                animationIndex = 0;
                animationIndex = 0;
                if (direction == Direction.UP)
                {
                    if (carryUp.Count > 0)
                    spriteRenderer.sprite = carryUp[carryUp.Count - 1];
                }
                else if (direction == Direction.LEFT)
                {
                    if (carryLeft.Count > 0)
                    spriteRenderer.sprite = carryLeft[carryLeft.Count - 1];
                }
                else if (direction == Direction.RIGHT)
                {
                    if (carryRight.Count > 0)
                    spriteRenderer.sprite = carryRight[carryRight.Count - 1];
                }
                else
                {
                    if (carryDown.Count > 0)
                    spriteRenderer.sprite = carryDown[carryDown.Count - 1];
                }
            }
        }
        else if (attackingInput)
        {
            rb.velocity = new Vector2(0, 0);
            currentTime = 0;
            animationIndex = 0;
            attacking = true;
            if (direction == Direction.DOWN)
            {
                spriteRenderer.sprite = walkingDown[animationIndex];

            }
            else if (direction == Direction.LEFT)
            {
                spriteRenderer.sprite = walkingLeft[animationIndex];
            }
            else if (direction == Direction.RIGHT)
            {
                spriteRenderer.sprite = walkingRight[animationIndex];
            }
            else
            {
                spriteRenderer.sprite = walkingUp[animationIndex];
                direction = Direction.UP;
            }
            attackingInput = false;
        }
        else
        {
            rb.velocity = new Vector2(horz, vert).normalized;
            float speedFactor = Time.deltaTime * mvAmt;
            rb.velocity = Vector2.Scale(rb.velocity, new Vector2(speedFactor, speedFactor));
            if (vert < -0.0001)
            {
                if (direction != Direction.DOWN)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    direction = Direction.DOWN;
                }

                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    animationIndex = animationIndex % walkingDown.Count;
                    currentTime = currentTime - animationSpeed;
                }
                spriteRenderer.sprite = walkingDown[animationIndex];
                currentTime = currentTime + Time.deltaTime;


            }
            else if (vert > 0.0001)
            {
                if (direction != Direction.UP)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    direction = Direction.UP;
                }

                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    animationIndex = animationIndex % walkingUp.Count;
                    currentTime = currentTime - animationSpeed;
                }
                spriteRenderer.sprite = walkingUp[animationIndex];
                currentTime = currentTime + Time.deltaTime;
                direction = Direction.UP;
            }
            else if (horz < -0.0001)
            {
                if (direction != Direction.LEFT)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    direction = Direction.LEFT;
                }

                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    animationIndex = animationIndex % walkingLeft.Count;
                    currentTime = currentTime - animationSpeed;
                }
                spriteRenderer.sprite = walkingLeft[animationIndex];
                currentTime = currentTime + Time.deltaTime;
                direction = Direction.LEFT;
            }
            else if (horz > 0.0001)
            {
                if (direction != Direction.RIGHT)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    direction = Direction.RIGHT;
                }

                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    animationIndex = animationIndex % walkingRight.Count;
                    currentTime = currentTime - animationSpeed;
                }
                spriteRenderer.sprite = walkingRight[animationIndex];
                currentTime = currentTime + Time.deltaTime;
                direction = Direction.RIGHT;
            }

            else
            {
                currentTime = 0;
                animationIndex = 0;
                animationIndex = 0;
                if (direction == Direction.UP)
                {
                    spriteRenderer.sprite = standingUp;
                }
                else if (direction == Direction.LEFT)
                {
                    spriteRenderer.sprite = standingLeft;
                }
                else if (direction == Direction.RIGHT)
                {
                    spriteRenderer.sprite = standingRight;
                }
                else
                {
                    spriteRenderer.sprite = standingDown;
                }
            }
        }
    }

    public bool getInteract()
    {
        return interact;
    }
}


