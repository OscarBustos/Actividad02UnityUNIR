using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializar audio
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip soundName)
    {
        audioSource.PlayOneShot(soundName); //Poner el clip que se le pase
    }
    
    public void Mute()
    {
        if((audioSource != null) && (audioSource.mute)) //Si el audio está muted
        {
            audioSource.mute = false; //Desmutear
            AudioListener.volume = 1.0f;
        }
        else if((audioSource != null) && (!audioSource.mute)) //Si el audio no está muted
        {
            audioSource.mute = true; //Mutear
            AudioListener.volume = 0.0f;
        }
    }
}
