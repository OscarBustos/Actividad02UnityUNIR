using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    [SerializeField] private CollectibleType collectibleType;
    [SerializeField] private int amount;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private IEnumerator HandleCollected() {
        audioSource.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject, 0.5f);
    }

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().CollectObject(collectibleType, amount);
            StartCoroutine(HandleCollected());
            
        }
    }
    #endregion
}
