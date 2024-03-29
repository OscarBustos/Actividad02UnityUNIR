using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifesUIController : MonoBehaviour
{
    [SerializeField] GameObject[] lifesHUD;
    [SerializeField] Sprite spriteLife;
    [SerializeField] Sprite spriteLooseLife;

    [SerializeField] TextMeshProUGUI progressText; //Texto de info
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLifes(int lifes)
    {
        if(lifes == 3)
        {
            lifesHUD[0].gameObject.GetComponent<Image>().overrideSprite = spriteLife;
            lifesHUD[1].gameObject.GetComponent<Image>().overrideSprite = spriteLife;
            lifesHUD[2].gameObject.GetComponent<Image>().overrideSprite = spriteLife;
        }
        else if(lifes == 2) //Si el player tiene 2 vidas
        {
            lifesHUD[0].gameObject.GetComponent<Image>().overrideSprite = spriteLife;
            lifesHUD[1].gameObject.GetComponent<Image>().overrideSprite = spriteLife;
            lifesHUD[2].gameObject.GetComponent<Image>().overrideSprite = spriteLooseLife;
        }
        else if(lifes == 1) //Si el player tiene 1 vida
        {
            lifesHUD[0].gameObject.GetComponent<Image>().overrideSprite = spriteLife;
            lifesHUD[1].gameObject.GetComponent<Image>().overrideSprite = spriteLooseLife;
            lifesHUD[2].gameObject.GetComponent<Image>().overrideSprite = spriteLooseLife;
        }
        else if(lifes == 0) //Si el player tiene 0 vidas
        {
            lifesHUD[0].gameObject.GetComponent<Image>().overrideSprite = spriteLooseLife;
        }
    }

    public void UpdateProgress(int progress, int goal)
    {
        progressText.text = "Progress:"+progress.ToString()+"/"+ goal.ToString();
    }

    public void WriteStartingProgress(int goal)
    {
        progressText.text = "Progress:0/"+goal.ToString();
    }
}
