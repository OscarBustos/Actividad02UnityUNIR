using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private Checkpoint[] checkpoints;
    private Vector3 spawnPoint;
    [SerializeField] private PlayerController player;

    public static CheckpointController instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>();
        spawnPoint = player.GetPosition();
    }

    public void DeactivateCheckpoints()
    {
        for(int i = 0; i < checkpoints.Length; i++) 
        {
            checkpoints[i].ResetCheckpoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    public Vector3 GetSpawnPoint() 
    { 
        return spawnPoint; 
    }
}
