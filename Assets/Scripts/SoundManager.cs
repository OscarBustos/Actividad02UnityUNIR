using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //Inicializar audio
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip soundName)
    {
        audioSource.PlayOneShot(soundName); //Poner el clip que se le pase
    }
}
