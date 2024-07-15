using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private LoginUI _loginUI;
    [SerializeField] private RegisterUI _registerUI;
    [SerializeField] private UpdateNameUI _nameWindow;
    private void Start()
    {
        OpenLogin();
    }
    public void OpenRegister()
    {
        _registerUI.gameObject.SetActive(true);
        _loginUI.gameObject.SetActive(false);
        _nameWindow.gameObject.SetActive(false);
    }

    public void OpenLogin()
    {
        _loginUI.gameObject.SetActive(true);
        _registerUI.gameObject.SetActive(false);
        _nameWindow.gameObject.SetActive(false);
    }
    public void OpenLogin(string username = "", string password = "")
    {
        _loginUI.gameObject.SetActive(true);
        _loginUI.SetInputLogin(username, password);
        _registerUI.gameObject.SetActive(false);
        _nameWindow.gameObject.SetActive(false);
    }
    public void OpenNameWindow()
    {
        _nameWindow.gameObject.SetActive(true);
        _loginUI.gameObject.SetActive(false);
        _registerUI.gameObject.SetActive(false);
    }


}
