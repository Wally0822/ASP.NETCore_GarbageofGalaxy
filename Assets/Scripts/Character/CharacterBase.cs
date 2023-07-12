using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
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

    public virtual void Move(float moveSpeed)
    {
        // �̵� ���� ����
    }

    // player, monster ���� ���� ����
    protected abstract void Die();
}
