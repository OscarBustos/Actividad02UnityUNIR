using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CurrentLevel", 1);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGame()
    {
        StartCoroutine(Wait());
        if(GameManager.Instance != null)
        {
            GameManager.Instance.Reload();
        }
        SceneManager.LoadScene("Level01");
        PlayerPrefs.SetInt("CurrentLevel",1);
    }

    public void ContinueGame()
    {
        StartCoroutine(Wait());
        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(PlayerPrefs.GetInt("CurrentLevel")).name);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    public void ReturnToMainMenu()
    {
        GameManager.Instance.Reload();
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
    }

    IEnumerator Wait()
    {
        // suspend execution for 2 seconds
        yield return new WaitForSeconds(10.0f);
    }

}
