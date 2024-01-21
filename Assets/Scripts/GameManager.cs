using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private float currentTime;
    private int currentTimeInSeconds;
    private int currentTimeInMinutes;
    private int currentTimeInHours;
    private const int TIME_UNIT = 60;

    [SerializeField] private bool gameStarted = false;

    #region Events
    public Action<string> OnTimeChanged;
    #endregion

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            currentTime = Time.timeSinceLevelLoad;
            currentTimeInMinutes = (int)currentTime / TIME_UNIT;
            currentTimeInHours = (int)currentTimeInMinutes / TIME_UNIT;
            currentTimeInSeconds = (int)currentTime - (currentTimeInMinutes * TIME_UNIT);
            OnTimeChanged?.Invoke("Time on game: " + currentTimeInHours + ":" + currentTimeInMinutes + ":" + currentTimeInSeconds);
        }        
    }
}
