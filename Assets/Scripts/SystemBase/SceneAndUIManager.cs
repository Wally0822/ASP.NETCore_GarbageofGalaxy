using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class SceneAndUIManager : UIBase
{
    private EventSystem _eventSystem;

    // ��Ÿ�� �ʱ�ȭ ������ SceneManager ����
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InstSceneManager()
    {
        var sceneAndUIManager = new GameObject().AddComponent<SceneAndUIManager>();
        sceneAndUIManager.name = "SceneAndUIManager";
    }

    protected override void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Unitask�� Ȱ���� �񵿱� �� �ε� �� �񵿱� UI ����
    public async UniTask LoadAsync(EnumTypes.Scenes scene)
    {
        // UI �񵿱� ����
        UniTask creatUIUnitask = CreatUI(scene);

        // �� �񵿱� �ε�
        UniTask loadSceneUnitask = LoadScene(scene);

        // �ش� ���� �ε� �۾� �� UI���� �۾� �Ϸ� ��
        await UniTask.WhenAll(creatUIUnitask, loadSceneUnitask);
    }

    private async UniTask CreatUI(EnumTypes.Scenes scene)
    {
        switch (scene)
        {
            case EnumTypes.Scenes.SceneInGame:
                // SceneInGame UI ���� �Լ� ȣ��
                InitGameUI();
                break;
            case EnumTypes.Scenes.SceneLobby:
                // SceneLobby UI ���� �Լ� ȣ��
                InitLobbyUI();
                break;
            case EnumTypes.Scenes.SceneTitle:
                // SceneTitle UI ���� �Լ� ȣ��
                InitTitleUI();
                break;
            default:
                Debug.LogError("Require scene name resolution");
                break;
        }
        await UniTask.CompletedTask;
    }

    private async UniTask LoadScene(EnumTypes.Scenes scene)
    {
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(scene.ToString());

        // Scene Load�� �Ϸ�� ������ ���
        while (!sceneLoad.isDone)
        {
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
    }


    private void InitGameUI()
    {
        UIManager.Instance.CreateUIObject("InGame", EnumTypes.LayoutType.Middle);
    }

    private void InitLobbyUI()
    {

    }

    private void InitTitleUI()
    {

    }

    public override IProcess.NextProcess ProcessInput()
    {
        return IProcess.NextProcess.Continue;
    }
}
