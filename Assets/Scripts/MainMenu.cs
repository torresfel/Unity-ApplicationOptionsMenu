using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animation _mainAnimator;
    [SerializeField] private AnimationClip _fadeOutAnimation;
    [SerializeField] private AnimationClip _fadeInAnimation;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OnFadeOutComplete()
    {
        Debug.LogWarning("FaceOut Complete");
        gameManager.UnloadLevel("Boot");
    }

    public void OnFadeInComplete()
    {
        Debug.LogWarning("FaceIn Complete");
    }

    public void FadeIn()
    {
        _mainAnimator.Stop();
        _mainAnimator.clip = _fadeInAnimation;
        _mainAnimator.Play();
    }

    public void FadeOut()
    {
        _mainAnimator.Stop();
        _mainAnimator.clip = _fadeOutAnimation;
        _mainAnimator.Play();
    }
}
