using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite checkpointOn, checkpointOff;


    #region Methods

    /// ------------------------------------------------------------------------------------------------------------------------
    /// Methods
    /// ------------------------------------------------------------------------------------------------------------------------
    
    public void ResetCheckpoint()
    {
        spriteRenderer.sprite = checkpointOff;
    }

    #endregion


    #region Events

    /// ------------------------------------------------------------------------------------------------------------------------
    /// Events
    /// ------------------------------------------------------------------------------------------------------------------------
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CheckpointController.instance.DeactivateCheckpoints();
            spriteRenderer.sprite = checkpointOn;
            CheckpointController.instance.SetSpawnPoint(transform.position);
        }
    }

    #endregion
}
