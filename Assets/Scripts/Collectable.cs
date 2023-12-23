using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    [SerializeField] CollectableType collectableType;


    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Todo: When collision with Player, execute player's collect method.
    }
    #endregion
}
