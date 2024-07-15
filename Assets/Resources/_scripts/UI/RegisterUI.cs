using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
public class RegisterUI : MonoBehaviour
{
    [SerializeField] private InputField _userNameInput;
    [SerializeField] private InputField _emailInput;
    [SerializeField] private InputField _passwordInput;
    [SerializeField] private MenuUIManager _uiManager;
    public void Register()
    {
        MySceneManager.Instance.SetWaitPanel(true);
        if (_passwordInput.text.Length < 6)
        {
            Debug.Log("Password too short!!!");
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {

            Username = _userNameInput.text,
            Email = _emailInput.text,
            Password = _passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        MySceneManager.Instance.SetWaitPanel(false);
        _uiManager.OpenLogin(_userNameInput.text, _passwordInput.text);
    }
    private void OnError(PlayFabError error)
    {
        MySceneManager.Instance.SetWaitPanel(false);

    }
}
