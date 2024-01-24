using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtBoxController : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //collision.transform.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            StartCoroutine(DeathEffect(collision));
        }
    }

    #region Coroutines
    private IEnumerator DeathEffect(Collider2D collision)
    {
        deathEffect.SetActive(true);
        GameObject instantiatedEffect = Instantiate(deathEffect, collision.transform.position, collision.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        Destroy(instantiatedEffect);
    }
    #endregion
}
