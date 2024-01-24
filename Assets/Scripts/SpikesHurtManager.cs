using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesHurtManager : MonoBehaviour
{
    [SerializeField] int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WallHurtBox"))
        {
            collision.gameObject.GetComponentInParent<PlayerController>().Hurt(1);
        }
    }
}
