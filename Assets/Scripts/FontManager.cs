using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontManager : MonoBehaviour
{

    [SerializeField] private Dropdown fontSizeSelector;
    [SerializeField] private ColorPaletteController colorPalette;
    private int size = 25;
    private Text[] texts;

    private void Awake()
    {
        fontSizeSelector.value = 1;
        colorPalette.OnColorChange.AddListener(OnFontColorChange);
        texts = FindObjectsOfType<Text>();
    }

    private void OnFontColorChange(Color color)
    {
        foreach(var text in texts)
            if(text != null)
                text.color = color;
    }

    public void OnFontSizeChange()
    {
        switch (fontSizeSelector.value)
        {
            case 0:
                //Small
                size = 15;
                break;
            case 1:
                size = 25;
                //Medium
                break;
            case 2:
                size = 35;
                //Large
                break;
        }
        ChangeFontSize();
    }

    private void ChangeFontSize()
    {
        var texts = FindObjectsOfType<Text>();
        foreach (var text in texts)
        {
            if(text != null)
                text.fontSize = size;
        }
    }
}
