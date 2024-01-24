using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private float waitToRespawn = 1f;
    [SerializeField] private PlayerController player;
    [SerializeField] private LifesUIController lifesUI;

    public static LevelManager instance;


    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Methods

    /// ------------------------------------------------------------------------------------------------------------------------
    /// Methods
    /// ------------------------------------------------------------------------------------------------------------------------

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private void PlayerChanges()
    {
        player.SetPosition(CheckpointController.instance.GetSpawnPoint());
        //player.SetRigidBody();
        //player.SetIsGrounded(true);
        player.SetLives(player.GetLives() - 1);
        lifesUI.UpdateLifes(player.GetLives());
        player.GetGameObject().SetActive(true);
    }

    #endregion


    #region Coroutines

    /// ------------------------------------------------------------------------------------------------------------------------
    /// Coroutines
    /// ------------------------------------------------------------------------------------------------------------------------

    IEnumerator RespawnCoroutine()
    {
        player.GetGameObject().SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);
        PlayerChanges();
    }

    #endregion
}
