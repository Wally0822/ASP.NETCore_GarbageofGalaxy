using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using APIModels;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class APIManager : MonoSingleton<APIManager>
{
    public APIDataSO apidata = null;

    private string _id;
    private string _authToken;

    private void Awake()
    {
        if (apidata == null)
        {
            apidata = Resources.Load<APIDataSO>("APIData");
        }
    }

    public async UniTask CreateAccpuntAPI(User user)
    {
        await CallAPI<Dictionary<string, object>, User>(APIUrls.CreateAccountApi, user, null);
    }

    public async UniTask LoginAPI(User user)
    {
        _id = user.ID;

#if UNITY_EDITOR
        Debug.Log($"_id : {_id}");
#endif
        await CallAPI<Dictionary<string, object>, User>(APIUrls.LoginApi, user, HandleLoginResponse);
    }

    private void HandleLoginResponse(APIResponse<Dictionary<string, object>> apiResponse)
    {
        var responseBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(apiResponse.responseBody);

        if (responseBody.TryGetValue("authToken", out var authTokenObj))
        {
            _authToken = authTokenObj as string;
        }

        if (_authToken != null && _authToken != string.Empty)
        {
            apidata.SetResponseData("GameData", NewGameData());
        }
        else
        {
            Debug.LogError("Failed to retrieve authToken from API response.");
        }
    }

    private GameData NewGameData()
    {
        GameData gameData = new GameData
        {
            ID = _id,
            AuthToken = _authToken
        };
        return gameData;
    }

    public GameData GetApiSODicUerData()
    {
        GameData curUserInfo = apidata.GetValueByKey<GameData>("GameData");

        if (curUserInfo == null)
        {
            Debug.LogError("No GameData found in APIDataSO.");
        }

        return curUserInfo;
    }

    public async UniTask GetGameDataAPI()
    {
        try
        {
            await CallAPI<Dictionary<string, object>, GameData>(APIUrls.GameDataApi, GetApiSODicUerData(), HandleGameDataResponse);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error calling GameData API: {e.Message}");
        }
    }

    private void HandleGameDataResponse(APIResponse<Dictionary<string, object>> apiResponse)
    {
        var responseBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(apiResponse.responseBody);

        if (responseBody.TryGetValue("playerData", out var playerDataObj) && playerDataObj is JArray)
        {
            List<PlayerData> playerDataList = new List<PlayerData>();

            foreach (var playerDataJToken in playerDataObj as JArray)
            {
                PlayerData playerData = playerDataJToken.ToObject<PlayerData>();
                playerDataList.Add(playerData);
            }

            apidata.SetResponseData("PlayerData", playerDataList);
        }
        else
        {
            Debug.LogError("Failed PlayerData from API response.");
        }
    }

    public async UniTask GetRanking()
    {
        await CallAPI<Dictionary<string, object>, GameData>(APIUrls.RankingApi, GetApiSODicUerData(), HandleRankingDataResponse);
    }

    private void HandleRankingDataResponse(APIResponse<Dictionary<string, object>> apiResponse)
    {
        var responseBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(apiResponse.responseBody);

        if (responseBody.TryGetValue("rankingData", out object rankingDataObj))
        {
            List<RankingData> rankingDataList =
                JsonConvert.DeserializeObject<List<RankingData>>(rankingDataObj.ToString());

            apidata.SetResponseData("RankingData", rankingDataList);
        }
        else
        {
            Debug.LogError("Failed RankingData from API response.");
        }
    }

    private async UniTask CallAPI<T, TRequest>(string apiUrl, TRequest requestBody, Action<APIResponse<T>> handler)
    {
        try
        {
            var apiResponse = await APIWebRequest.PostAsync<T>(apiUrl, requestBody);

            if (apiResponse == null)
            {
                Debug.LogError("API Response Data is null");
            }

            handler?.Invoke(apiResponse);
        }

        catch (UnityWebRequestException e)
        {
            Debug.LogError($"API request failed : {e.Message}");
        }
    }

    private async UniTask AddRankingDataAsync(string responseBody)
    {
        // JSON ���� �Ľ�
        JObject jsonResponse = await UniTask.Run(() => JObject.Parse(responseBody));
        JArray rankingDataArray = (JArray)jsonResponse["rankingData"];

        if (rankingDataArray == null)
        {
            Debug.LogError("��ŷ ������ �迭�� ��� �ֽ��ϴ�.");
            return;
        }

        // ��ŷ �����͸� �� ����Ʈ�� �߰�
        foreach (JObject rankingData in rankingDataArray)
        {
            string id = (string)rankingData["id"];
            int? score = (int?)rankingData["score"];
            int? ranking = (int?)rankingData["ranking"];

            if (id != null && score.HasValue && ranking.HasValue)
            {
                //_rankIdList.Add(id);
                //_rankScoreList.Add(score.Value);
                //_rankingList.Add(ranking.Value);
                //Debug.Log(_rankIdList);
            }
            else
            {
                Debug.LogWarning("��ŷ �����Ϳ� null�� �ְų� �� ����");
            }
        }
    }
}