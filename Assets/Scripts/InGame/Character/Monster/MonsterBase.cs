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
    }

    public override void Move()
    {
    }

    public override void Attack()
    {
        // ���� ���� ����
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
    }

    private void init()
    {

    }

    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
    }

}
