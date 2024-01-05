using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementPlatform : EnemyController
{
    [SerializeField] private Transform groundController;
    
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        distance = 1.31f;
        isRight = true;
    }

    private void FixedUpdate()
    {
        if (!IsDead())
        {

            RaycastHit2D infoGround = Physics2D.Raycast(groundController.position, Vector2.down, distance);

            rb.velocity = new Vector2(speed, rb.velocity.y);

            if (infoGround == false)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        isRight = !isRight;
        transform.eulerAngles = new Vector2(0, transform.eulerAngles.y + 180);
        speed *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundController.transform.position, groundController.transform.position + Vector3.down * distance);
    }
}
