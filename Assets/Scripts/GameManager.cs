using System;
using UnityEngine;
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
    private bool reload;

    private bool gameOver = false;

    [SerializeField] private bool gameStarted = false;

    
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private GameObject playerSpawnPoint;

    [SerializeField] private GameObject gameOverCanvace;
    [SerializeField] private GameObject winCanvace;

    private string timeOnGame;

    #region Events
    public Action<string> OnTimeChanged;
    #endregion

    private void Awake()
    {
        Time.timeScale = 1.0f;
        ChangePlayerSpawningPoint(playerSpawnPoint);
        if (instance == null || reload)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            if (PlayerPrefs.GetInt("GameStarted") == 0)
            {
                Time.timeScale = 1;
                PlayerPrefs.SetInt("GameStarted", 1);
                PlayerPrefs.SetInt("CanMove", 0);
                PlayerPrefs.SetInt("CanJump", 0);
                PlayerPrefs.SetInt("CanDoubleJump", 0);
                PlayerPrefs.SetInt("CanWallJump", 0);
            }
            reload = false;
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
        if (!gameOver)
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

    public void ChangePlayerSpawningPoint(GameObject spawnPoint)
    {
        PlayerPrefs.SetFloat("SpawnPointX", spawnPoint.transform.position.x);
        PlayerPrefs.SetFloat("SpawnPointy", spawnPoint.transform.position.y);
    }

    public void GameOver()
    {
        gameOver = true;
        timeOnGame = "Time on game: " + currentTimeInHours + ":" + currentTimeInMinutes + ":" + currentTimeInSeconds;
        gameOverCanvace.SetActive(true);
    }

    public void Reload()
    {
        Destroy(gameObject);
    }
    public string getTimeOnGame()
    {
        return timeOnGame;
    }

    public void Win()
    {
        winCanvace.SetActive(true);
    }
}
