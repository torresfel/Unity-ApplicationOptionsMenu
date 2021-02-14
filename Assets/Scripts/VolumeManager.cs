using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Text volumeQtyText;

    private GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        volumeQtyText.text = int.Parse(volumeSlider.value.ToString()).ToString();
    }

    public void OnVolumeChange()
    {
        var volume = volumeSlider.value;
        volumeQtyText.text = int.Parse(volume.ToString()).ToString();
        gameManager.ChangeVolume(volume/100);
    }
}
