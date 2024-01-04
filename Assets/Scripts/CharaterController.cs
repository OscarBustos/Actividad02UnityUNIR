using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterController : MonoBehaviour
{
    protected int lives;
    protected int points;
    protected float speed;

    protected bool isRight = true;
    private bool dead = false;
    protected bool isGrounded; 

    private SpriteRenderer playerRenderer;
    protected Rigidbody2D rb;

    private AudioSource sound;

    private void Awake()
    {
        playerRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();       
    }


    private void Sound()
    {
        sound.Play();
    }

    protected void Flip(int direction)
    {
        if (direction > 0)
        {
            if (!playerRenderer.flipX)
            {
                Sound();
            }
            playerRenderer.flipX = true;
            isRight = true;
        }
        else if (direction < 0)
        {
            if (playerRenderer.flipX)
            {
                Sound();
            }
            playerRenderer.flipX = false;
            isRight = false;
        }
    }

    /// ------------------------------------------------------------------------------------------------------------------------
    /// Getters and setters
    /// ------------------------------------------------------------------------------------------------------------------------
    /// 

    public int GetLives()
    {
        return lives;
    }

    public void SetLives(int num)
    {
        lives = num;
    }

    public int GetPoints()
    {
        return points;
    }

    public void SetPoints(int num)
    {
        points = num;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float num)
    {
        speed = num;
    }

    public bool IsDead()
    {
        if (lives <= 0)
        {
            dead = true;
            //gameObject.SetActive(false);
        }
        else
        {
            dead = false;
        }

        return dead;
    }
}
