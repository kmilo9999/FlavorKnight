using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{




    // This controls how quickly the player moves
    public float mvAmt = 5;

    // This is the index for the walking animation
    private int walkingIndex;


    // This is the sprite to use when standing still
    public Sprite standingUp;
    // This is a list of sprites to use when moving
    public List<Sprite> walkingUp;
    // Keeping track of what direction is being used
    private bool isWalkingUp;


    // This is the sprite to use when standing still
    public Sprite standingRight;
    // This is a list of sprites to use when moving
    public List<Sprite> walkingRight;
    // Keeping track of what direction is being used
    private bool isWalkingRight;

    // This is the sprite to use when standing still
    public Sprite standingLeft;
    // This is a list of sprites to use when moving
    public List<Sprite> walkingLeft;
    // Keeping track of what direction is being used
    private bool isWalkingLeft;

    // This is the sprite to use when standing still
    public Sprite standingDown;
    // This is a list of sprites to use when moving
    public List<Sprite> walkingDown;
    // Keeping track of what direction is being used
    private bool isWalkingDown;





    // This is the renderer of the player
    public SpriteRenderer spriteRenderer;

    // How quickly the next frame is advanced for walking, in seconds
    public float animationSpeed;





    private float currentTime;

    private bool interact = false;



    // This keeps track of horizontal movement
    private float horz;
    // This keeps track of vertical movement
    private float vert;

    // This is the rigid body that controls the physics of the player
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        walkingIndex = 0;
        walkingIndex = 0;
        currentTime = 0;
        isWalkingUp = false;
        // I want to initialize this as true, so the character starts facing downward
        isWalkingDown = true;
        isWalkingRight = false;
        isWalkingLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        horz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        if (Input.GetKeyDown("space"))
        {
            interact = true;
            Debug.Log("interact " + interact);
        }
        else
        {
            interact = false;
        }
    }

   

    // This is called for every physics update
    void FixedUpdate()
    {
        

        

        rb.velocity = new Vector2(horz, vert).normalized;
        float speedFactor = Time.deltaTime * mvAmt;
        rb.velocity = Vector2.Scale(rb.velocity, new Vector2(speedFactor, speedFactor));
        if (vert < -0.0001)
        {
            if (isWalkingUp) {
                currentTime = 0;
                walkingIndex = 0;
                isWalkingUp = false;
            }
            if (isWalkingRight) {
                currentTime = 0;
                walkingIndex = 0;
                isWalkingRight = false;
            }

            if (isWalkingLeft) {
                currentTime = 0;
                walkingIndex = 0;
                isWalkingLeft = false;
            }

            if (currentTime > animationSpeed)
            {
                walkingIndex++;
                walkingIndex = walkingIndex % walkingDown.Count;
                currentTime = currentTime - animationSpeed;
            }
            spriteRenderer.sprite = walkingDown[walkingIndex];
            currentTime = currentTime + Time.deltaTime;
            isWalkingDown = true;
            
        } else if (vert > 0.0001) {
            if (isWalkingDown) {
                currentTime = 0;
                walkingIndex = 0;
                isWalkingDown = false;
            }
            if (isWalkingRight)
            {
                currentTime = 0;
                walkingIndex = 0;
                isWalkingRight = false;
            }

            if (isWalkingLeft)
            {
                currentTime = 0;
                walkingIndex = 0;
                isWalkingLeft = false;
            }

            if (currentTime > animationSpeed)
            {
                walkingIndex++;
                walkingIndex = walkingIndex % walkingUp.Count;
                currentTime = currentTime - animationSpeed;
            }
            spriteRenderer.sprite = walkingUp[walkingIndex];
            currentTime = currentTime + Time.deltaTime;
            isWalkingUp = true;
        }
        else if (horz < -0.0001)
        {
            if (isWalkingDown)
            {
                currentTime = 0;
                walkingIndex = 0;
                isWalkingDown = false;
            }
            if (isWalkingUp)
            {
                currentTime = 0;
                walkingIndex = 0;
                isWalkingUp = false;
            }
            if (isWalkingRight)
            {
                currentTime = 0;
                walkingIndex = 0;
                isWalkingRight = false;
            }

            if (currentTime > animationSpeed)
            {
                walkingIndex++;
                walkingIndex = walkingIndex % walkingUp.Count;
                currentTime = currentTime - animationSpeed;
            }
            spriteRenderer.sprite = walkingLeft[walkingIndex];
            currentTime = currentTime + Time.deltaTime;
            isWalkingLeft = true;
        }
        else if (horz > 0.0001)
        {
            if (isWalkingDown)
            {
                currentTime = 0;
                walkingIndex = 0;
                isWalkingDown = false;
            }
            if (isWalkingUp)
            {
                currentTime = 0;
                walkingIndex = 0;
                isWalkingUp = false;
            }
            if (isWalkingLeft)
            {
                currentTime = 0;
                walkingIndex = 0;
                isWalkingLeft = false;
            }

            if (currentTime > animationSpeed)
            {
                walkingIndex++;
                walkingIndex = walkingIndex % walkingUp.Count;
                currentTime = currentTime - animationSpeed;
            }
            spriteRenderer.sprite = walkingRight[walkingIndex];
            currentTime = currentTime + Time.deltaTime;
            isWalkingRight = true;
        }

        else
        {
            currentTime = 0;
            walkingIndex = 0;
            walkingIndex = 0;
            if (isWalkingUp) {
                spriteRenderer.sprite = standingUp;
            } else if (isWalkingLeft){
                spriteRenderer.sprite = standingLeft;
            } else if (isWalkingRight) {
                spriteRenderer.sprite = standingRight;
            } else {
                spriteRenderer.sprite = standingDown;
            }
        }
    }

    public bool getInteract()
    {
        return interact;
    }
}


