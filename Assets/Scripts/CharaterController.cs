using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterController : MonoBehaviour
{
    [SerializeField] protected int lives = 1;
    [SerializeField] protected int points;
    [SerializeField] protected float speed;

    protected bool isRight = true;
    [SerializeField] private bool dead = false;
    protected bool isGrounded; 

    private SpriteRenderer playerRenderer;
    protected Rigidbody2D rb;

    private AudioSource sound;
    protected Animator animator;

    private void Awake()
    {
        playerRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    #region Methods

    private void PlaySound()
    {
        sound.Play();
    }

    protected void Flip(int direction)
    {
        if (direction > 0)
        {
            if (!playerRenderer.flipX)
            {
                PlaySound();
            }
            playerRenderer.flipX = true;
            isRight = true;
        }
        else if (direction < 0)
        {
            if (playerRenderer.flipX)
            {
                PlaySound();
            }
            playerRenderer.flipX = false;
            isRight = false;
        }
    }

    #endregion

    #region Getters and Setters
    
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

    public bool IsDead() => dead = lives <= 0 || dead ? true : false;
    #endregion
}