using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = AudioListener.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ControlVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
