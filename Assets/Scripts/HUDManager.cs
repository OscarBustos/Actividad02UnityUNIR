using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeInGameText;

    private void Awake()
    {
        GameManager.Instance.OnTimeChanged += OnTimeChanged;
    }
    
    private void OnTimeChanged(string text)
    {
        timeInGameText.text = text;
    }

    
}
