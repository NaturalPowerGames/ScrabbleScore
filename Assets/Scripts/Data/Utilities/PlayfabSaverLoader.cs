using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayfabSaverLoader
{
    private string targetUser;

    public PlayfabSaverLoader()
	{
#if UNITY_ANDROID
        LoginWithDeviceID();
#else
 // Create a random CustomID if one doesn't already exist
        string customID = PlayerPrefs.GetString("PlayFabCustomID", System.Guid.NewGuid().ToString());

        // Save CustomID so it persists for future sessions
        PlayerPrefs.SetString("PlayFabCustomID", customID);

        // Log in with the CustomID
        var request = new LoginWithCustomIDRequest
        {
            CustomId = customID,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
#endif
    }

    public void LoginWithDeviceID()
    {
#if UNITY_ANDROID
        var request = new LoginWithAndroidDeviceIDRequest
        {
            AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithAndroidDeviceID(request, OnLoginSuccess, OnLoginFailure);
#elif UNITY_IOS
    var request = new LoginWithIOSDeviceIDRequest
    {
        DeviceId = SystemInfo.deviceUniqueIdentifier,
        CreateAccount = true
    };
    PlayFabClientAPI.LoginWithIOSDeviceID(request, OnLoginSuccess, OnLoginFailure);
#endif
    }
    public void UploadToPlayFab(GameData gameData)
    {
        // Convert this instance to JSON
        string jsonData = JsonUtility.ToJson(gameData);

        // Prepare data for PlayFab upload
        var requestData = new Dictionary<string, string>
        {
            { gameData.MatchID.ToString(), jsonData }
        };

        // Upload data to PlayFab
        var request = new UpdateUserDataRequest
        {
            Data = requestData
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataUploadSuccess, OnDataUploadError);
    }

    private void OnDataUploadSuccess(UpdateUserDataResult result)
    {
        Debug.Log("GameData successfully uploaded to PlayFab.");
    }

    private void OnDataUploadError(PlayFabError error)
    {
        Debug.LogError("Error uploading GameData: " + error.GenerateErrorReport());
    }

    public void DownloadAllGameData(string targetUser)
    {
        this.targetUser = targetUser;
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataDownloaded, OnDataDownloadError);
    }

    private void OnDataDownloaded(GetUserDataResult result)
    {
        List<GameData> gamesForUser = new List<GameData>();

        foreach (var entry in result.Data)
        {
            GameData gameData = JsonUtility.FromJson<GameData>(entry.Value.Value);

            if (gameData.Players.Any((x)=> x.name == targetUser))
            {
                gamesForUser.Add(gameData);
            }
        }
        DataEvents.OnGamesLoadedForUser?.Invoke(gamesForUser.ToArray());
        Debug.Log($"{gamesForUser.Count} games found for the specified user.");
    }

    private void OnDataDownloadError(PlayFabError error)
    {
        Debug.LogError("Error retrieving data: " + error.GenerateErrorReport());
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Logged in successfully with CustomID!");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Error logging in: " + error.GenerateErrorReport());
    }
}
