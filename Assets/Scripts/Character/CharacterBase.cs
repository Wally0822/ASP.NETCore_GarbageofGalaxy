using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerInfo
{
    public string CharacterName;
    public int NowHP;
    public int MaxHp;
    public int NowMP;
    public int MaxMp;
    public int Lv;
}

public abstract class CharacterBase : MonoBehaviour
{
    private PlayerInfo _playerInfo;

    // ���� ������ ���� ���� ĳ���͸� Ȥ�� ���͸�
    public string CharacterName;
    public int hp;
    public int mp;
    public int lv;


    protected virtual void Start()
    {
        // �ڽĿ� �ǿ� ȣ�� ������ �ʱ�ȭ �۾�
    }

    // player, monster ���� ���� ����
    public abstract void Attack();

    public abstract void Move();


    // player, monster ���� ���� ����
    protected abstract void Die();
}
