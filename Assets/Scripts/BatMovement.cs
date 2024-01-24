using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : EnemyController
{

    [SerializeField] private Transform[] leftPathPoints;
    [SerializeField] private Transform[] rightPathPoints;
    private Transform initialPosition;
    private Vector3 lastPosition;

    private float distance;
    private bool flying;
    private bool returnToSleep;
    private Vector3 currentTarget;
    private int targetIndex;
    private int playerDirection;
    private int movementDirection = 1;
    private bool movementStarted;
    private bool originalFlip;



    void Start()
    {
        isRight = !spriteRenderer.flipX;
        initialPosition = leftPathPoints[0];
        initialPosition.parent.SetParent(null);
        originalFlip = isRight;
    }

    private void Update()
    {
        Move();
        HandleDeath();
    }

    #region Methods

    private void Move()
    {
        if (flying && !IsDead())
        {
            Transform[] pathPoints = playerDirection > 0 ? rightPathPoints : leftPathPoints;
            if(targetIndex >= pathPoints.Length)
            {
                targetIndex = pathPoints.Length - 1;
            }
            else if(targetIndex < 0)
            {
                targetIndex = 0;
            }
            lastPosition = pathPoints[pathPoints.Length - 1].position;
            currentTarget = pathPoints[targetIndex].position;
            if(transform.position == currentTarget)
            {
                if(currentTarget == lastPosition || (currentTarget == initialPosition.position && movementStarted))
                {
                    if(currentTarget == initialPosition.position && returnToSleep)
                    {
                        animator.SetBool("Fly", false);
                        flying = false;
                        returnToSleep = false;
                        //spriteRenderer.flipX = originalFlip;
                        //isRight = originalFlip;
                        Flip();
                        return;
                    }
                    Flip();

                } 
               targetIndex += movementDirection;
            }
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
            movementStarted = true;
        }
    }
    private void Flip()
    {
        isRight = !isRight;
        movementDirection *= -1;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
    #endregion

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float collisionDirection = (collision.transform.position - transform.position).normalized.x;
            playerDirection = collisionDirection > 0 ? 1 : -1;
            spriteRenderer.flipX = playerDirection > 0 ? false : true;
            animator.SetBool("Fly", true);
            flying = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float collisionDirection = (collision.transform.position - transform.position).normalized.x;
            
            int newDirection = collisionDirection > 0 ? 1 : -1;
            if (newDirection != playerDirection)
            {
                playerDirection = newDirection;
                Flip();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            returnToSleep = true;
        }
    }
    #endregion
}
