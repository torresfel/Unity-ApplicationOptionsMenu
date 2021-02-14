using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject[] SystemPrefabs;

    List<GameObject> _instancedSystemPrefabs;
    List<AsyncOperation> _loadOperations;
    private AudioSource _audioSource;
    private string _currentLevel = string.Empty;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        _instancedSystemPrefabs = new List<GameObject>();
        _loadOperations = new List<AsyncOperation>();
        _audioSource = GetComponent<AudioSource>();

        InstantiateSystemPrefabs();

        LoadLevel("OptionsMenu");
    }

    internal void ChangeVolume(float volume)
    {
        _audioSource.volume = volume;
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);
        }
        FindObjectOfType<LangResolver>().ResolveTexts();
        Debug.Log("Load complete.");
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload complete.");
    }

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        foreach(var go in SystemPrefabs)
        {
            prefabInstance = Instantiate(go);
            _instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    private void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if(ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);

        _currentLevel = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if(ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload level " + levelName);
            return;
        }

        ao.completed += OnUnloadOperationComplete;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _instancedSystemPrefabs.ForEach(Destroy);

        _instancedSystemPrefabs.Clear();
    }
}
