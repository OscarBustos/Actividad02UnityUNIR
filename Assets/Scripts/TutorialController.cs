using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Image image;
    private Image parentImage;

    [SerializeField] UnityEvent ExecuteOnTrigger;

    // Start is called before the first frame update
    void Start()
    {
        parentImage = image.rectTransform.parent.gameObject.GetComponent<Image>();
        EnableImage(parentImage, false);
        EnableImage(image, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si el player colisiona con el triger del tutorial
        if (collision.gameObject.CompareTag("Player")) {
            EnableImage(parentImage, true);
            EnableImage(image, true);
            ExecuteOnTrigger?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //si el player sale del triger del tutorial
        if (collision.gameObject.CompareTag("Player"))
        {
            EnableImage(parentImage, false);
            EnableImage(image, false);
        }
    }

    private void EnableImage(Image image, bool enabled)
    {
        image.enabled = enabled;
    }
}
