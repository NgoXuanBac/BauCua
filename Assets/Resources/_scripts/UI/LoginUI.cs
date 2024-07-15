using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class LoginUI : MonoBehaviour
{
    [SerializeField] private readonly InputField _emailOrUsernameInput;
    [SerializeField] private readonly InputField _passwordInput;
    [SerializeField] private readonly MenuUIManager _uiManager;
    
    private bool IsValidEmail(string strIn)
    {
        // Return true if strIn is in valid e-mail format.
        return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }
    public void Login()
    {
        MySceneManager.Instance.SetWaitPanel(true);
        if (IsValidEmail(_emailOrUsernameInput.text))
        {
            var request = new LoginWithEmailAddressRequest
            {
                Email = _emailOrUsernameInput.text,
                Password = _passwordInput.text,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetPlayerProfile = true
                }
            };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
        }
        else
        {
            var request = new LoginWithPlayFabRequest
            {
                Username = _emailOrUsernameInput.text,
                Password = _passwordInput.text,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetPlayerProfile = true
                }
            };
            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
        }

    }
    public void SetInputLogin(string username, string password)
    {
        _emailOrUsernameInput.text = username;
        _passwordInput.text = password;
    }

    private void OnLoginSuccess(LoginResult result)
    {
        MySceneManager.Instance.SetWaitPanel(false);
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        if (name != null)
        {
            SaveSystem.SetString("CURRENT_NAME", name);
            MySceneManager.Instance.LoadScene(1);
        }
        else
            _uiManager.OpenNameWindow();

    }
    private void OnError(PlayFabError error)
    {
        MySceneManager.Instance.SetWaitPanel(false);
    }

}
