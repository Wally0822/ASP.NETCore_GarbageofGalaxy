using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public override void Attack()
    {
       /* if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject bullet = ObjectPooler.SpawnFromPool("Bullet", gameObject.transform.position);
        }*/
        if (Input.GetMouseButtonDown(0)) // AŰ�� Move�� ���ļ� ���콺 ��Ŭ������ �׽�Ʈ �����غþ��
        {
            GameObject bullet = ObjectPooler.SpawnFromPool("Bullet", gameObject.transform.position);
        }
    }
}
