using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private float waitToRespawn = 1f;
    [SerializeField] private PlayerController player;

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

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        player.GetGameObject().SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);
        player.GetGameObject().SetActive(true);
        player.SetPosition(CheckpointController.instance.GetSpawnPoint());
        player.SetLives(player.GetMaxLives());
    }
}
