using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : CharacterBase
{
    float monsterSpeed;
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        Move(monsterSpeed);
    }

    public override void Move(float moveSpeed)
    {
        base.Move(moveSpeed);
    }

    public override void Attack()
    {
        // ���� ���� ����
    }

    protected override void Die()
    {
        // ���� ����
    }

    private void init()
    {

    }
}
