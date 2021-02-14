using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    private bool isLoading = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isLoading)
        {
            isLoading = true;
            _mainMenu.FadeOut();
        }
    }
}
