using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    [SerializeField] private float countDown = 3;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private IEnumerator StartFalling()
    {
        
        yield return new WaitForSeconds(countDown);
            //countDown -= Time.deltaTime;
        rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        rigidBody2D.mass = 10;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StartFalling());
        }
    }
}
