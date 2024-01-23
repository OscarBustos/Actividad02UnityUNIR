using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Image image;

    // Start is called before the first frame update
    void Start()
    {
        image.enabled = false; //Escondemos las imagenes del tutorial

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si el player colisiona con el triger del tutorial
        if (collision.gameObject.CompareTag("Player")) {
            image.enabled = true; //Activamos la imagen
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //si el player sale del triger del tutorial
        if (collision.gameObject.CompareTag("Player"))
        {
            image.enabled = false; //Desactivamos la imagen
        }
    }
}
