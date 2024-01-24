using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : CharaterController
{
    // Start is called before the first frame update
    void Start()
    {
        lives = 1;
    }

    // Update is called once per frame
    void Update()
    {
        HandleDeath();
    }


    #region Methods

    /// ------------------------------------------------------------------------------------------------------------------------
    /// Methods
    /// ------------------------------------------------------------------------------------------------------------------------

    public void HandleDeath()
    {
        if (IsDead())
        {
            StartCoroutine(Die());
        }
    }
    #endregion


    #region Coroutines

    /// ------------------------------------------------------------------------------------------------------------------------
    /// Coroutines
    /// ------------------------------------------------------------------------------------------------------------------------
    private IEnumerator Die()
    {
        // Todo: play particles, sound and animations
        animator.SetBool("Die", true);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        //animator.SetBool("Die", false);
        Destroy(gameObject);
    }
    #endregion
}
