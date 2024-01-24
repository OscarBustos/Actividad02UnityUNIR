using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneController : MonoBehaviour
{

    #region Events

    /// ------------------------------------------------------------------------------------------------------------------------
    /// Events
    /// ------------------------------------------------------------------------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            LevelManager.instance.RespawnPlayer();
        }
    }

    #endregion

}
