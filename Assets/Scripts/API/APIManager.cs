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

    // Game Data
    private string _id;
    private string _authToken;

    // Player Data
    private long _player_uid;
    private int _exp;
    private int _hp;
    private int _score;
    private int _level;
    private int _status;

    // Ranking Data
    private List<string> _rankIdList = new List<string>();
    private List<int> _rankScoreList = new List<int>();
    private List<int> _rankingList = new List<int>();

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

        if (_authToken != null)
        {
            apidata.SetResponseData("GameData", NewGameData());
        }

        else
        {
            Debug.LogError("Failed to retrieve authToken from API response.");
        }
    }

    public async UniTask GetGameDataAPI()
    {
        await CallAPI<Dictionary<string, object>, GameData>(APIUrls.GameDataApi, NewGameData(), null);
    }

    public async UniTask GetRanking()
    {
        await CallAPI<Dictionary<string, object>, GameData>(APIUrls.RankingApi, NewGameData(), null);
    }

    private void HandleRankingDataResponse(APIResponse<Dictionary<string, object>> apiResponse)
    {
        // APIWebRequest.ParseResponseBodyToModel<RankingData[]>(apiResponse.responseBody, "rankingData");
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

    private async UniTask CallAPI<T, TRequest>(string apiUrl, TRequest requestBody, Action<APIResponse<T>> handler)
    {
        try
        {
            var apiResponse = await APIWebRequest.PostAsync<T>(apiUrl, requestBody);

            if (apiResponse == null)
            {
                Debug.LogError("API ������ null�Դϴ�");
            }
            else
            {
                string responseBody = apiResponse.responseBody;

                switch (apiUrl)
                {
                    case var url when url == APIUrls.GameDataApi:
                        
                        break;
                    case var url when url == APIUrls.RankingApi:
                        await AddRankingDataAsync(apiResponse.responseBody);
                        break;
                    default:
                        break;
                }
            }

            handler?.Invoke(apiResponse);
        }

        catch (UnityWebRequestException e)
        {
            Debug.LogError($"API request failed : {e.Message}");
        }
    }
    /*
        private PlayerData NewPlayerData()
        {
            PlayerData playerData = new PlayerData
            {
                player_uid = _player_uid,
                exp = _exp,
                hp = _hp,
                score = _score,
                level = _level,
                status = _status
            };
            return playerData;
        }
    */
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
                _rankIdList.Add(id);
                _rankScoreList.Add(score.Value);
                _rankingList.Add(ranking.Value);
                Debug.Log(_rankIdList);
            }
            else
            {
                Debug.LogWarning("��ŷ �����Ϳ� null�� �ְų� �� ����");
            }
        }
    }
}