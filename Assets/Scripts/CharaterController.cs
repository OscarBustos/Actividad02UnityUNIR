using UnityEngine;

public class CharaterController : MonoBehaviour
{
    [SerializeField] protected int lives = 1;
    [SerializeField] protected int points;
    [SerializeField] protected float speed;

    protected bool isRight = true;
    [SerializeField] private bool dead = false;
    protected bool isGrounded; 

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;

    private AudioSource sound;
    protected Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    #region Methods

    /// ------------------------------------------------------------------------------------------------------------------------
    /// Methods
    /// ------------------------------------------------------------------------------------------------------------------------

    protected void PlaySound()
    {
        sound.Play();
    }

    protected void Flip(int direction)
    {
        if (direction < 0)
        {
            spriteRenderer.flipX = true;
            isRight = true;
        }
        else if (direction > 0)
        {
            spriteRenderer.flipX = false;
            isRight = false;
        }
    }

    #endregion


    #region Getters and Setters

    /// ------------------------------------------------------------------------------------------------------------------------
    /// Getters and Setters
    /// ------------------------------------------------------------------------------------------------------------------------

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