using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public int OpenLevel;
    public int OpenCondition;
    [SerializeField] Animator fadeCanvasAnimator;

    private IEnumerator LoadScene()
    {
        fadeCanvasAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(OpenLevel);
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
    
}
