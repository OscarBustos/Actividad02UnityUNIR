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
            collision.transform.gameObject.SetActive(false);
            //deathEffect.transform.gameObject.SetActive(true);
            //Instantiate(deathEffect, collision.transform.position, collision.transform.rotation);
            //deathEffect.transform.gameObject.SetActive(false);
            StartCoroutine(DeathEffect(collision));
        }
    }

    #region Coroutines
    private IEnumerator DeathEffect(Collider2D collision)
    {
        deathEffect.transform.gameObject.SetActive(true);
        Instantiate(deathEffect, collision.transform.position, collision.transform.rotation);
        deathEffect.transform.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        //deathEffect.transform.gameObject.SetActive(false);
    }
    #endregion
}
