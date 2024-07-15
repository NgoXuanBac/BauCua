// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using PlayFab;
// using PlayFab.ClientModels;
// using System;

// public class PlayfabManager : MonoBehaviour
// {


//     public void SendLeaderBoard(int score)
//     {
//         var request = new UpdatePlayerStatisticsRequest
//         {
//             Statistics = new List<StatisticUpdate>(){
//                 new StatisticUpdate{
//                     StatisticName="Richest",
//                     Value=score
//                 }
//             }
//         };

//         PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnError);
//     }

//     private void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result)
//     {
//         Debug.Log("Leader Board Update");
//     }

//     public void GetLeaderBoard()
//     {
//         var request = new GetLeaderboardRequest
//         {
//             StatisticName = "Richest",
//             StartPosition = 0,
//             MaxResultsCount = 10
//         };
//         PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboard, OnError);
//     }

//     private void OnGetLeaderboard(GetLeaderboardResult result)
//     {
//         foreach (var item in result.Leaderboard)
//         {
//             Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
//         }
//     }

//     public void SaveAppearance(int score)
//     {
//         var request = new UpdateUserDataRequest
//         {
//             Data = new() { { "MONEY", score.ToString() } }
//         };
//         PlayFabClientAPI.UpdateUserData(request, OnSaveAppearance, OnError);
//     }

//     private void OnSaveAppearance(UpdateUserDataResult obj)
//     {
//         Debug.Log("Success Save Data");
//     }
//     public void GetAppearance(string nameData)
//     {
//         var request = new GetUserDataRequest
//         {
//             Keys = new() { nameData }
//         };
//         PlayFabClientAPI.GetUserData(request, OnGetAppearance, OnError);

//     }

//     private void OnError(PlayFabError error)
//     {
//         Debug.Log(error);
//     }

//     private void OnGetAppearance(GetUserDataResult result)
//     {
//         if (result == null)return;
        
//         // string score = result.Data["Score"].Value.ToString();
//         // Debug.Log(score);
//     }


// }
