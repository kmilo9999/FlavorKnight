using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{




    // This controls how quickly the player moves
    public float mvAmt = 5;

    // This is the index for the walking animation
    private int animationIndex;


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

    // Sprites for the respective attack directions
    public List<Sprite> attackUp;
    public List<Sprite> attackRight;
    public List<Sprite> attackLeft;
    public List<Sprite> attackDown;



    public KeyCode attackKey = KeyCode.J;



    // This is the renderer of the player
    public SpriteRenderer spriteRenderer;

    // How quickly the next frame is advanced for walking, in seconds
    public float animationSpeed;





    private float currentTime;




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


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animationIndex = 0;
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
        attackingInput = Input.GetKeyDown(KeyCode.J);
    }

    // This is called for a fixed time update (not by frame)
    void FixedUpdate()
    {
        if (attacking)
        {
            if (isWalkingUp)
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
            else if (isWalkingRight)
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
            else if (isWalkingLeft)
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
        else if (attackingInput)
        {
            currentTime = 0;
            animationIndex = 0;
            attacking = true;
            if (isWalkingDown)
            {
                spriteRenderer.sprite = walkingDown[animationIndex];

            }
            else if (isWalkingLeft)
            {
                spriteRenderer.sprite = walkingLeft[animationIndex];
            }
            else if (isWalkingRight)
            {
                spriteRenderer.sprite = walkingRight[animationIndex];
            }
            else
            {
                spriteRenderer.sprite = walkingUp[animationIndex];
                isWalkingUp = true;
            }
        }
        else
        {
            rb.velocity = new Vector2(horz, vert).normalized;
            float speedFactor = Time.deltaTime * mvAmt;
            rb.velocity = Vector2.Scale(rb.velocity, new Vector2(speedFactor, speedFactor));
            if (vert < -0.0001)
            {
                if (isWalkingUp)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    isWalkingUp = false;
                }
                if (isWalkingRight)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    isWalkingRight = false;
                }

                if (isWalkingLeft)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    isWalkingLeft = false;
                }

                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    animationIndex = animationIndex % walkingDown.Count;
                    currentTime = currentTime - animationSpeed;
                }
                spriteRenderer.sprite = walkingDown[animationIndex];
                currentTime = currentTime + Time.deltaTime;
                isWalkingDown = true;

            }
            else if (vert > 0.0001)
            {
                if (isWalkingDown)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    isWalkingDown = false;
                }
                if (isWalkingRight)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    isWalkingRight = false;
                }

                if (isWalkingLeft)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    isWalkingLeft = false;
                }

                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    animationIndex = animationIndex % walkingUp.Count;
                    currentTime = currentTime - animationSpeed;
                }
                spriteRenderer.sprite = walkingUp[animationIndex];
                currentTime = currentTime + Time.deltaTime;
                isWalkingUp = true;
            }
            else if (horz < -0.0001)
            {
                if (isWalkingDown)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    isWalkingDown = false;
                }
                if (isWalkingUp)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    isWalkingUp = false;
                }
                if (isWalkingRight)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    isWalkingRight = false;
                }

                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    animationIndex = animationIndex % walkingUp.Count;
                    currentTime = currentTime - animationSpeed;
                }
                spriteRenderer.sprite = walkingLeft[animationIndex];
                currentTime = currentTime + Time.deltaTime;
                isWalkingLeft = true;
            }
            else if (horz > 0.0001)
            {
                if (isWalkingDown)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    isWalkingDown = false;
                }
                if (isWalkingUp)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    isWalkingUp = false;
                }
                if (isWalkingLeft)
                {
                    currentTime = 0;
                    animationIndex = 0;
                    isWalkingLeft = false;
                }

                if (currentTime > animationSpeed)
                {
                    animationIndex++;
                    animationIndex = animationIndex % walkingUp.Count;
                    currentTime = currentTime - animationSpeed;
                }
                spriteRenderer.sprite = walkingRight[animationIndex];
                currentTime = currentTime + Time.deltaTime;
                isWalkingRight = true;
            }

            else
            {
                currentTime = 0;
                animationIndex = 0;
                animationIndex = 0;
                if (isWalkingUp)
                {
                    spriteRenderer.sprite = standingUp;
                }
                else if (isWalkingLeft)
                {
                    spriteRenderer.sprite = standingLeft;
                }
                else if (isWalkingRight)
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
}


