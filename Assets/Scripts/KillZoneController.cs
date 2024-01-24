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
        if(
            (collision.CompareTag("Player") && gameObject.layer == LayerMask.NameToLayer("Default")) || 
            (collision.CompareTag("WallHurtBox") && gameObject.layer == LayerMask.NameToLayer("Spikes"))
        )
        {
            LevelManager.instance.RespawnPlayer();
        }
    }

    #endregion

}
