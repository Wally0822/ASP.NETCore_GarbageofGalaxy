using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
interface ISkillBase
{
    void SkillLevelUp();
    void SkillShot();
}
public abstract class SkillBase : MonoBehaviour, ISkillBase
{
    protected Player _player;
    protected int _skillLevel = 1;
    protected float _coolTime = 10f;
    protected bool _isCool = false;
    protected KeyCode _shotKey;
    protected UI_SceneGame _uI_SceneGame;
    public KeyCode ShotKey { get { return _shotKey; } set { _shotKey = value; } }
  
    #region Unity LifeCycle
    protected virtual void Start()
    {
        _player = gameObject.GetComponent<Player>();
        _uI_SceneGame = FindObjectOfType<UI_SceneGame>();
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetKeyDown(_shotKey))
        {
            if (_isCool == false)
            {
                _isCool = true;

                SkillShot();

                _uI_SceneGame.SetCoolTime(_shotKey, _coolTime, () => _isCool = false);
            }
        }
    }

    #endregion
    /// <summary>
    /// _skillLevel++ ���ֽð� ��ų �������� ���� ��� ���� ����
    /// </summary>
    public abstract void SkillLevelUp();
    /// <summary>
    /// ��ų ���� �����Ӱ� �ۼ����ּ���
    /// </summary>
    public abstract void SkillShot();
    /// <summary>
    /// ��ų ��Ÿ�� _coolTime ���⿡ float���� �ð� �Ҵ� ���ּ��� 
    /// </summary>
    public abstract void SkillCoolTime();
}
