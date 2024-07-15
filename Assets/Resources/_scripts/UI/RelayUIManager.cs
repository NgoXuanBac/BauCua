using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class RelayUIManager : MonoBehaviour
{
    [SerializeField] private Text _noticeText;
    [SerializeField] private Button _joinBtn;
    [SerializeField] private Button _createBtn;
    [SerializeField] private InputField _joinCodeInput;
    [SerializeField] private RelayServices _relay;

    private void Start()
    {
        _createBtn.onClick.AddListener(() => CreateRoom());
        _joinBtn.onClick.AddListener(() => JoinRoom());
        _noticeText.text = "Hello " + SaveSystem.GetString("CURRENT_NAME");
    }

    private async void JoinRoom()
    {
        if (_joinCodeInput.text == "") return;
        MySceneManager.Instance.SetWaitPanel(true);
        bool condition = await _relay.JoinRelay(_joinCodeInput.text);
        if (condition)
        {
            SaveSystem.SetString("JOIN_CODE", _joinCodeInput.text);
            MySceneManager.Instance.OnNextScene(() => MySceneManager.Instance.SetWaitPanel(false), "Game");
        }
        else MySceneManager.Instance.SetWaitPanel(false);
    }
    private async void CreateRoom()
    {
        MySceneManager.Instance.SetWaitPanel(true);
        string joinCode = await _relay.CreateRelay();
        if (joinCode == null)
        {
            MySceneManager.Instance.SetWaitPanel(false);
            return;
        }
        SaveSystem.SetString("JOIN_CODE", joinCode);
        Debug.Log(joinCode);
        NetworkManager.Singleton.SceneManager.OnSceneEvent += OnSceneEvent;
        NetworkManager.Singleton.SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    private void OnSceneEvent(SceneEvent sceneEvent)
    {
        if (sceneEvent.SceneEventType == SceneEventType.LoadComplete)
            MySceneManager.Instance.OnNextScene(() => MySceneManager.Instance.SetWaitPanel(false), "Game");
    }
}
