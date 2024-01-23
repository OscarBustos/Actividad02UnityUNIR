using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : CharaterController
{
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private int maxJumps = 2; 
    private int numJumps;

    [Header("Animator")]
    private Animator anim;

    private int direction;
    private bool jump;
    private bool jumped;
    private float onAirTime;
    private bool falling;

    [SerializeField] private bool canMove;
    [SerializeField] private bool canJump;
    [SerializeField] private bool canDoubleJump;
    [SerializeField] private bool canWalkJump;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        //speed = 5f;
        numJumps = 0;
        //lives = 5;
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        Move();
        if (!isGrounded)
        {
            isGrounded = IsGrounded();
        }
    }
    private void FixedUpdate()
    {
        if (IsDead())
        {
            //GameManager.Instance.GameOver(false);
        }
        else
        {
            
            HorizontalMovement(direction);
            StandUp();
            Jump();
            anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
            anim.SetBool("isGrounded", isGrounded);
        }        
    }

    #region Methods

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) && canMove)
        {
            direction = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && canMove)
        {
            direction = 1;
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && canJump)
        {
            jump = true;
        }
    }


    void HorizontalMovement(int direction)
    {
        Vector2 targetVelocity = new Vector2(speed * Input.GetAxis("Horizontal"), rb.velocity.y);
        Vector3 velocity = Vector3.zero;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
        Flip(direction);
    }

    private void Jump()
    {
        
        if (!falling)
        {
            onAirTime = 0;
        }
        else
        {
            onAirTime += Time.fixedDeltaTime;
        }

        if (jump)
        {
            bool canJump = false;
            if (falling && !jumped)
            {
                canJump = (onAirTime > 0 && onAirTime < 0.25f) && numJumps == maxJumps ? true : false;
            }
             
            
           if (isGrounded /*|| canJump/* || DoubleJump()*/)
            {
                
                Debug.Log("Before jumped " + jumped + " IsGrounded " + isGrounded + " onAirTime " + onAirTime + " DoubleJump " + DoubleJump() + " NumJumps " + numJumps + " Falling " + falling);

                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumped = true;
                numJumps++;
                isGrounded = false;
                Debug.Log("After jumped " + jumped + " IsGrounded " + isGrounded + " onAirTime " + onAirTime + " DoubleJump " + DoubleJump() + " NumJumps " + numJumps + " Falling " + falling);
            }
        }       
        jump = false;

    }


    private bool DoubleJump()
    {
        if (!isGrounded && !falling && (numJumps < maxJumps))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StandUp()
    {
        transform.localEulerAngles = Vector2.zero;
    }

    public void CollectObject(CollectibleType collectibleType, int amount)
    {
        switch (collectibleType)
        {
            case CollectibleType.Point: 
                { 
                    points += amount;
                    break;
                }

            case CollectibleType.Lifes:
                {
                    lives += amount;
                    break;
                }
        }
        
    }

    private bool IsGrounded()
    {
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (grounded)
        {
            numJumps = 0;
            jumped = false;
            falling = false;
        } else if (!grounded && !jumped)
        {
            falling = true;
            numJumps = 2;
        }
        return grounded;
    }
    #endregion


    #region Collisions
     
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isGrounded)
        {
            IsGrounded();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            lives --;
        }
    }

    #endregion


    #region setters and getters
    public void SetCanMove()
    {
        canMove = true;
    }

    public void SetCanJump()
    {
        canJump = true;
    }

    public void SetCanDoubleJump()
    {
        canDoubleJump = true;
    }

    public void SetCanWallJump()
    {
        canWalkJump = true;
    }
    #endregion
}
