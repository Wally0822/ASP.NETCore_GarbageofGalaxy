using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : CharacterBase
{
    [Header("User Setting")]
    [SerializeField] float playerSpeed;

    Rigidbody playerRb;

    protected override void Start()
    {
        // ��Ÿ �ʱ�ȭ �׸� �߰�
        base.Start();
    }

    private void Awake()
    {
        InitComponent();
        InitSetting();
    }

    void Update()
    {
        Move();
    }

    public override void Attack()
    {
        // ���� ���� �� �߰� �׸�
    }

    protected override void Die()
    {
        // player Die
    }
}
