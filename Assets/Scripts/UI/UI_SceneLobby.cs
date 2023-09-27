public class UI_SceneLobby : UIBase
{
    IProcess.NextProcess _nextProcess = IProcess.NextProcess.Continue;
    public override IProcess.NextProcess ProcessInput()
    {
        return _nextProcess;
    }

    protected override void Awake()
    {
        SoundMgr.Instance.BGMPlay(EnumTypes.StageBGMType.Lobby);
    }

    public async void OnClick_GameStart()
    {
        SoundMgr.Instance.SFXPlay(EnumTypes.SFXType.Button);
        await GameManager.Instance.MoveSceneWithAction(EnumTypes.ScenesType.SceneInGame, OnHide);
    }

    UI_Attendance uI_Attendance;
    public void OnClick_Attendance()
    {
        SoundMgr.Instance.SFXPlay(EnumTypes.SFXType.Button);

        if(uI_Attendance == null)
        {
            uI_Attendance = UIManager.Instance.CreateObject<UI_Attendance>("UI_Attendance", EnumTypes.LayoutType.Middle);
        }

        OnHide();
        uI_Attendance.OnShow();
    }

    UI_GPopupOption _gPopupOption = null;
    public void OnClick_Options()
    {
        SoundMgr.Instance.SFXPlay(EnumTypes.SFXType.Button);
        OnHide();
        if (_gPopupOption == null)
        {
            _gPopupOption = UIManager.Instance.CreateObject
              <UI_GPopupOption>("GPopup_Options", EnumTypes.LayoutType.Global);
            _gPopupOption.uI_SceneLobby = this;
        }
        _gPopupOption.OnShow();
    }
    RankUI _rankUi = null;
    public void OnClick_RankingList()
    {
        SoundMgr.Instance.SFXPlay(EnumTypes.SFXType.Button);
        OnHide();
        if (_rankUi == null)
        {
            _rankUi = UIManager.Instance.CreateObject
              <RankUI>("Popup_Ranking", EnumTypes.LayoutType.Middle);
            _rankUi.uI_SceneLobby = this;
        }
        _rankUi.OnShow();
    }

    public void OnClick_QuitBtn()
    {
        UIManager.Instance.OnClickQuitBtn();
    }

    public async void OnClick_LogOut()
    {
        SoundMgr.Instance.SFXPlay(EnumTypes.SFXType.Button);
        bool result = await APIManager.Instance.LogOutAPI();
        if(result)
        {
            await GameManager.Instance.MoveSceneWithAction(EnumTypes.ScenesType.SceneTitle, OnHide);
        }
    }
}