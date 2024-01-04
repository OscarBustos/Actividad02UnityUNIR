using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : CharaterController
{
    private float jumpForce = 6f;
    private int maxJumps = 2; 
    private int numJumps; 


    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        speed = 5f;
        numJumps = 0;
        lives = 5;
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
        }
        
    }

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
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
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

    /// ------------------------------------------------------------------------------------------------------------------------
    /// Events
    /// ------------------------------------------------------------------------------------------------------------------------
    ///

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
}
