using System;
using APIModels;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public PlayerData playerData { get; set; }
    public int StageNum { get; set; }

    Texture2D _cursorImg;

    #region Unity Lifecycle
    protected void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        _cursorImg = Resources.Load<Texture2D>("Sprites/cursor");
        Cursor.SetCursor(_cursorImg, Vector2.zero, CursorMode.Auto);
    }
    #endregion

   
    public int GetStageNum()
    {
        return StageNum;
    }

    public async void EndStage(int stageNum)
    {
        Debug.Log("승리");
        await MoveSceneWithAction(EnumTypes.ScenesType.SceneLobby);
    }

    public async UniTask MoveSceneWithAction(EnumTypes.ScenesType scene, Action actionBeforeLoad = null)
    {
        if (actionBeforeLoad != null)
        {
            actionBeforeLoad.Invoke();
        }

        try
        {
            var sceneLoad = SceneManager.LoadSceneAsync(scene.ToString());
            await UniTask.WaitUntil(() => sceneLoad.isDone);
            UIManager.Instance.OnSceneCallUI(scene);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading scene: {e.Message}");
        }
    }
}