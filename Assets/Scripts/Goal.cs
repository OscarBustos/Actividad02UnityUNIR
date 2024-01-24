using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public int OpenLevel;
    public int OpenCondition;
    [SerializeField] Animator fadeCanvasAnimator;

    private IEnumerator LoadScene()
    {
        
        if(OpenLevel == 100)
        {
            PlayerPrefs.SetInt("CanMove", 0);
            PlayerPrefs.SetInt("CanJump", 0);
            PlayerPrefs.SetInt("CanDoubleJump", 0);
            PlayerPrefs.SetInt("CanWallJump", 0);
            GameManager.Instance.Win();
        } else
        {
            fadeCanvasAnimator.SetTrigger("FadeOut");
            PlayerPrefs.SetInt("CanMove", 1);
            PlayerPrefs.SetInt("CanJump", 1);
            PlayerPrefs.SetInt("CanDoubleJump", 1);
            PlayerPrefs.SetInt("CanWallJump", 1);
            yield return new WaitForSeconds(1f);
            PlayerPrefs.SetInt("CurrentLevel", OpenLevel);
            SceneManager.LoadScene(OpenLevel);
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerController> ().GetPoints() == OpenCondition)
            {
                StartCoroutine(LoadScene());
            }
           
        }
    }

    public int GetOpenCondition()
    {
        return OpenCondition;
    }

}
