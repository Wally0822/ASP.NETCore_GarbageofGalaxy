using TMPro;
using UnityEngine;

public class UI_SceneTitle : UIBase
{
    [SerializeField] TextMeshProUGUI versionText = null;

    IProcess.NextProcess _nextProcess = IProcess.NextProcess.Continue;
    public override IProcess.NextProcess ProcessInput()
    {
        return _nextProcess;
    }

    #region Unity Lifecycle
    protected override void Awake()
    {
        SoundMgr.Instance.BGMPlay(EnumTypes.StageBGMType.Title);

        GetGameVersion();
        GetMasterData();
    }
    #endregion

    private async void GetGameVersion()
    {
        versionText.text = "Ver :  " + await APIManager.Instance.GetGameVersionAPI();
    }

    private async void GetMasterData()
    {
        await APIManager.Instance.GetMasterDataAPI();
    }
}