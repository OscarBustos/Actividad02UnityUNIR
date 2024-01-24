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
        player.GetGameObject().SetActive(true);
        player.SetHurt(false);
        player.SetPosition(CheckpointController.instance.GetSpawnPoint());
        player.SetLives(player.GetLives() - 1);
        lifesUI.UpdateLifes(player.GetLives());
        player.SetHurt(false);
    }

    #endregion


    #region Coroutines

    /// ------------------------------------------------------------------------------------------------------------------------
    /// Coroutines
    /// ------------------------------------------------------------------------------------------------------------------------

    IEnumerator RespawnCoroutine()
    {
        player.SetHurt(true);
        yield return new WaitForSeconds(1f);
        player.GetGameObject().SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);
        PlayerChanges();
    }

    #endregion
}
