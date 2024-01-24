using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] int horizontalDirection;
    [SerializeField] float maxHorizontalDistance;
    [SerializeField] int verticalDirection;
    [SerializeField] float maxVerticalDistance;
    [SerializeField] float speed;
    
    

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool moving;
    
    private void Awake()
    {
        originalPosition = transform.position;
    }
    
    void Update()
    {
        if (!moving)
        {
            float x = originalPosition.x + maxHorizontalDistance * horizontalDirection;
            float y = originalPosition.y + maxVerticalDistance * verticalDirection;
            targetPosition = new Vector3(x, y, 0);
            moving = true;
        } 
        else if (transform.position == targetPosition) {
            horizontalDirection *= -1;
            verticalDirection *= -1;
            moving = false;
            originalPosition = transform.position;
        } else if(moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    #region Collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
    #endregion
}
