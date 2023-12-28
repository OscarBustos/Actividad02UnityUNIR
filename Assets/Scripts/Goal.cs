using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update

    public int OpenLevel; //Indica el nivel que abre esa meta
    public int OpenCondition; //Indica la cantidad de coleccionables necesarios para que se abra

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* COMENTADO POR EL MOMENTO
    void ChangeLevel() //Función de cambio de nivel
    {
        if (OpenLevel== 2) //Al acabar el nivel 1 se pasa al nivel 2
        {
            SceneManager.LoadScene("Level2");
        }

        if (OpenLevel == 3) //Al acabar el nivel 2 se pasa al nivel 3
        {
            SceneManager.LoadScene("Level3");
        }

        if (OpenLevel == 0) //Al acabar el nivel 3 se vuelve al menú porque se completó el juego
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) //Funcion para detectar colisiones
    {
        if (collision.gameObject.tag == "Player") //Si el jugador entra en colisión con la meta
        {
            if(GameObject.Find("Player").GetComponent<Player > ().puntos == OpenCondition) //Si el jugador tiene los coleccionables necesarios
            {
                ChangeLevel(); //Llamamos a la funcion de cambio de nivel
            }
           
        }
    }
    FIN DEL COMENTARIO */ 
}
