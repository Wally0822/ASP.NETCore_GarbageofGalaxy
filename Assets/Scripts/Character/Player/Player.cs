using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase
{
    float playerSpeed;

    void Start()
    {
        // ��Ÿ �ʱ�ȭ �׸� �߰�
        base.Start();
        init();
    }

    void Update()
    {
        Move(playerSpeed);
    }

    public override void Attack()
    {
        // ���� ���� �� �߰� �׸�
    }

    protected override void Die()
    {
        // player Die
    }

    public override void Move(float moveSpeed)
    {
        base.Move(moveSpeed);
    }

    private void init()
    {
        // �ʱ�ȭ �׸�
    }
}
