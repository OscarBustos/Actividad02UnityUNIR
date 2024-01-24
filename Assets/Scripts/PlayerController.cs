using UnityEngine;

public class PlayerController : CharaterController
{
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float wallRayDistance = 1;
    private int maxJumps = 2; 
    private int numJumps;
    private int maxLives;

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


    private int fallingJumps = 0;
    private int maxFallingJumps = 1;
    private bool jumpedFromGround;
    private bool wallCollision;
    private bool leftWall;
    private bool rightWall;
    private int leftWallJumpCount;
    private int rightWallJumpCount;
    private JumpOrigin jumpOrigin;

    public enum JumpOrigin
    {
        Wall, Air, Ground
    }

    // Start is called before the first frame update
    void Start()
    {
        float x = PlayerPrefs.GetFloat("SpawnPointX");
        float y = PlayerPrefs.GetFloat("SpawnPointy");
        canMove = PlayerPrefs.GetInt("CanMove") == 1;
        canJump = PlayerPrefs.GetInt("CanJump") == 1;
        canDoubleJump = PlayerPrefs.GetInt("CanDoubleJump") == 1;
        canWalkJump = PlayerPrefs.GetInt("CanWallJump") == 1;
        transform.position = new Vector2(x, y);
        //isGrounded = true;
        //speed = 5f;
        numJumps = 0;
        //lives = 5;
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        Move();
        
    }
    private void FixedUpdate()
    {
        if (IsDead())
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            isGrounded = IsGrounded();
            HandleWallCollision();
            HorizontalMovement(direction);
            StandUp();
            HandleJump();
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
            PlaySound();
        }
    }


    void HorizontalMovement(int direction)
    {
        Vector2 targetVelocity = new Vector2(speed * Input.GetAxis("Horizontal"), rb.velocity.y);
        Vector3 velocity = Vector3.zero;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
        Flip(direction);
    }

    public void Hurt(int amount)
    {
        Debug.Log("Wast Hurt " + amount);
    }

    #region Jump
    private void HandleJump()
    {
        if (falling)
        {
            HandleFalling();
        }
        else if (isGrounded)
        {
            HandleJumpFromGround();
        }
        else if (wallCollision)
        {
            HandleJumpFromWall();
        }
        if (DoubleJump())
        {
            HandleDoubleJump();
        }
        jump = false;
    }

    private void HandleDoubleJump()
    {
        if (jump && canDoubleJump)
        {
            Jump();
            jumpOrigin = JumpOrigin.Air;
        }
    }

    private void HandleJumpFromWall()
    {
        if(jump && rightWall && rightWallJumpCount == 0 && direction == -1 && (jumpOrigin == JumpOrigin.Wall || jumpOrigin == JumpOrigin.Ground))
        {
            Jump();
            rightWallJumpCount++;
            jumpOrigin = JumpOrigin.Wall;
        }
        else if(jump && leftWall && leftWallJumpCount == 0 && direction == 1 && (jumpOrigin == JumpOrigin.Wall || jumpOrigin == JumpOrigin.Ground))
        {
            Jump();
            leftWallJumpCount++;
            jumpOrigin = JumpOrigin.Wall;
        }
    }

    private void HandleJumpFromGround()
    {
        if (jump)
        {
            Jump();
            jumpedFromGround = true;
            jumpOrigin = JumpOrigin.Ground;
        }
    }

    private void HandleFalling()
    {
        onAirTime += Time.fixedDeltaTime;
        
        if(jump && (onAirTime > 0 && onAirTime < 0.15f) &&  fallingJumps < maxFallingJumps)
        {
            Jump();
            fallingJumps++;
            jumpOrigin = JumpOrigin.Air;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        numJumps++;
        jumped = true;
    }

    private bool DoubleJump()
    {
        if (!isGrounded && !wallCollision && jumpOrigin != JumpOrigin.Air && (numJumps < maxJumps))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsGrounded()
    {
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        if (grounded)
        {

            onAirTime = 0;
            jumped = false;
            jumpedFromGround = false;
            falling = false;

            numJumps = 0;
            //Debug.Log("Grounded");
        }
        else if (!jumped && !jumpedFromGround)
        {
            numJumps = 1;
            //Debug.Log("falling jumped  " + jumped);
            falling = true;
        }
        return grounded;
    }

    private void HandleWallCollision()
    {
        if (!isGrounded)
        {
            if (IsCollidingWithWall(Vector3.right))
            {
                numJumps = 0;
                wallCollision = true;
                rightWall = true;
                leftWall = false;
                leftWallJumpCount = 0;
                //Debug.Log("rightWall count " + rightWallJumpCount);
            }
            else if (IsCollidingWithWall(Vector3.left))
            {
                numJumps = 0;
                wallCollision = true;
                leftWall = true;
                rightWall = false;
                rightWallJumpCount = 0;
                //Debug.Log("leftWall count " + leftWallJumpCount);
            }
            else
            {
                wallCollision = false;
                //Debug.Log("NoWall");
            }
        }
        else
        {
            wallCollision = false;
            leftWall = false;
            leftWallJumpCount = 0;
            rightWall = false;
            rightWallJumpCount = 0;
        }
    }

    private bool IsCollidingWithWall(Vector3 direction)
    {
        Debug.DrawLine(transform.position, transform.position + direction * wallRayDistance, Color.red);
        return Physics2D.Raycast(transform.position, direction, wallRayDistance, groundLayer);
    }

    #endregion
    private void StandUp()
    {
      //  transform.localEulerAngles = Vector3.zero;
    }

    public void DealDamage()
    {
        LevelManager.instance.RespawnPlayer();
    }
    
    public void UpdateProgress()
    {
        LevelManager.instance.UpdateGemsUI();
    }

    public void UpdateLifes()
    {
        LevelManager.instance.UpdateLifesUI();
    }

    #endregion


    #region Collisions

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
             DealDamage();
        }
    }

    public void CollectObject(CollectibleType collectibleType, int amount)
    {
        switch (collectibleType)
        {
            case CollectibleType.Point:
                {
                    points += amount;
                    UpdateProgress();
                    break;
                }

            case CollectibleType.Lifes:
                {
                    lives += amount;
                    UpdateLifes();
                    break;
                }
        }

    }

    #endregion

    #region Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, 0.1f);
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

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public int GetMaxLives()
    {
        return maxLives;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
    #endregion
}
