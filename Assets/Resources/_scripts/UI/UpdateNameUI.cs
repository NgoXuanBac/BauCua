using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Collections;

public class UpdateNameUI : MonoBehaviour
{
    [SerializeField] private InputField _nameInput;
    public void SubmitName()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = _nameInput.text
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayerNameUpdate, OnError);
    }
    private void OnDisplayerNameUpdate(UpdateUserTitleDisplayNameResult result)
    {

        Debug.Log("Update displayer name success");
        SaveSystem.SetString("CURRENT_NAME", _nameInput.text);
        MySceneManager.Instance.LoadScene(1);
    }
    private void OnError(PlayFabError error)
    {
        Debug.Log(error);
    }

}
