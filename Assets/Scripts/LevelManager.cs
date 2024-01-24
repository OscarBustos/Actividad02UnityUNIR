using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private float waitToRespawn = 1f;
    [SerializeField] private PlayerController player;
    [SerializeField] private LifesUIController playerUI;

    [SerializeField] private Goal goal;

    public static LevelManager instance;


    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        playerUI.WriteStartingProgress(goal.GetOpenCondition());
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
        Debug.Log(player.GetLives());
        player.GetGameObject().SetActive(true);
        UpdateLifesUI();
    }

    public void UpdateGemsUI()
    {
        playerUI.UpdateProgress(player.GetPoints(), goal.GetOpenCondition());
        
    }

    public void UpdateLifesUI()
    {
        playerUI.UpdateLifes(player.GetLives());
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
