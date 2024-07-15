using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MySceneManager : MonoBehaviour
{
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private GameObject _quitPanel;
    [SerializeField] private GameObject _waitPanel;
    [SerializeField] private Slider _progressBar;

    public static MySceneManager Instance { get; private set; }
    public bool IsActive { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetQuitPanel(true);
        }
    }
    public void SetQuitPanel(bool isActive)
    {
        IsActive = isActive;
        _quitPanel.SetActive(isActive);
    }
    public void SetWaitPanel(bool isActive = false)
    {
        IsActive = isActive;
        _waitPanel.SetActive(isActive);
    }

    public async void OnNextScene(Action action, string sceneName)
    {
        while (SceneManager.GetActiveScene().name != sceneName)
            await UniTask.WaitForEndOfFrame(this);
        action.Invoke();
    }
    private void TurnOffWaitPanel()
    {
        SetWaitPanel(false);
    }
    public async void LoadScene(int sceneIndex)
    {
        _progressBar.value = 0;
        var operation = SceneManager.LoadSceneAsync(sceneIndex);
        _loaderCanvas.SetActive(true);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            await UniTask.WaitForEndOfFrame(this);
            _progressBar.value = operation.progress;
            if (operation.progress >= 0.9f) operation.allowSceneActivation = true;
        }
        _loaderCanvas.SetActive(false);
    }

}
