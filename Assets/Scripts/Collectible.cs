using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    [SerializeField] private CollectibleType collectibleType;
    [SerializeField] private int amount;

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Todo: When collision with Player, execute player's collect method.
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<Player>().CollectObject(collectableType, );
            // Todo: play sound 
            gameObject.SetActive(false);
            Destroy(gameObject, 0.5f);
        }
    }
    #endregion
}
