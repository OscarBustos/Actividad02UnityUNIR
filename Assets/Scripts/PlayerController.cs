using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : CharaterController
{
    private float jumpForce = 6f;
    private int maxJumps = 2; 
    private int numJumps;

    [Header("Animator")]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        speed = 5f;
        numJumps = 0;
        lives = 5;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead())
        {
            //GameManager.Instance.GameOver(false);
        }
        else
        {
            Move();
            StandUp();
            anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
            anim.SetBool("isGrounded", isGrounded);
        }        
    }

    #region Methods

    /// ------------------------------------------------------------------------------------------------------------------------
    /// Methods
    /// ------------------------------------------------------------------------------------------------------------------------

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            HorizontalMovement(-1);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            HorizontalMovement(1);
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && (isGrounded || DoubleJump()))
        {
            Jump();
        }
    }


    void HorizontalMovement(int direction)
    {
        rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), rb.velocity.y);
        Flip(direction);
    }


    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        numJumps++;
        isGrounded = false;
    }


    private bool DoubleJump()
    {
        if (!isGrounded && (numJumps < maxJumps))
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
        transform.localEulerAngles = Vector3.zero;
    }

    public void DealDamage()
    {
        lives--;
        if (lives <= 0)
        {
            lives = 0;
            LevelManager.instance.RespawnPlayer();
        }
        //else
        //{

        //}
    }
    #endregion

    #region Getters and Setters

    /// ------------------------------------------------------------------------------------------------------------------------
    /// Getters and Setters
    /// ------------------------------------------------------------------------------------------------------------------------

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
    #endregion


    #region Events

    /// ------------------------------------------------------------------------------------------------------------------------
    /// Events
    /// ------------------------------------------------------------------------------------------------------------------------

    void OnCollisionEnter2D(Collision2D collision)
    {
        // The player is on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            numJumps = 0;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lives --;
        }
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

    #endregion
}
