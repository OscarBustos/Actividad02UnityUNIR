using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private float currentTime;
    private int currentTimeInSeconds;
    private int currentTimeInMinutes;
    private int currentTimeInHours;
    private const int TIME_UNIT = 60;
    
    private bool paused = false;

    [SerializeField] private bool gameStarted = false;
    
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;


    #region Events
    public Action<string> OnTimeChanged;
    #endregion

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
        currentTime += Time.deltaTime;
        currentTimeInMinutes = (int)currentTime / TIME_UNIT;
        currentTimeInHours = (int)currentTimeInMinutes / TIME_UNIT;
        currentTimeInSeconds = (int)currentTime - (currentTimeInMinutes * TIME_UNIT);
        OnTimeChanged?.Invoke("Time on game: " + currentTimeInHours + ":" + currentTimeInMinutes + ":" + currentTimeInSeconds);


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                resumeButton.onClick.Invoke();
                paused = false;
            }
            else
            {
                pauseButton.onClick.Invoke();
                paused = true;
            }
            
        }

    }
}
