using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [SerializeField] GameObject creditsText; //Texto creditos
    [SerializeField] GameObject tittleText; //Texto derrota
    [SerializeField] GameObject menu; //Menu 
    [SerializeField] GameObject info; //Info
    [SerializeField] TextMeshProUGUI infoText; //Texto de info
    private int time; //Tiempo restante para que salgan los creditos

    // Start is called before the first frame update
    void Start()
    {
        time = 5; //5 segundos de espera de créditos
        InvokeRepeating("UpdateInfo", 0f, 1f); //Mostrar los segundos que quedan al jugador
        Invoke("ActivateCredits", 5f); //Activar los créditos
        Invoke("ActivateMenu", 15f); //Activar el menú
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateInfo()
    {
        if(time > 0) //Si todavía no se han acabado los 5 segundos, mostrar el mensaje por pantalla
        {
            infoText.text = "In " + time.ToString() + " seconds you will see the credits.\nThank you so much for playing our game!";
            time -= 1;
        }
        else //Si ya se han acabado, desactivar el game object y el invoke a la función
        {
            info.SetActive(false);
            CancelInvoke("UpdateInfo");
        }

    }
    private void ActivateCredits()
    {
        tittleText.SetActive(false); //Desactivar el titulo
        creditsText.SetActive(true); //Activar los créditos
    }

    private void ActivateMenu()
    {
        creditsText.SetActive(false); //Desactivar los creditos
        tittleText.SetActive(true); //Activar el título
        menu.SetActive(true); //Activar el menú
    }
}
