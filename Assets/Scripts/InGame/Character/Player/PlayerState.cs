using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Player
{
    public override void Attack()
    {
        GameObject pullBullet = ObjectPooler.SpawnFromPool("Bullet2D", gameObject.transform.position);

        // Bullet�� �߻� ���� ����
        if (pullBullet.TryGetComponent<Bullet>(out Bullet bull))
        {
            bullet = bull;
        }
        else
        {
            bullet = pullBullet.AddComponent<Bullet>();
        }

        bullet.SetShooter(gameObject);

        if (pullBullet.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            bulletRb = rb;
        }
        else
        {
            bulletRb = pullBullet.AddComponent<Rigidbody2D>();
        }

        // �ӵ� ����ġ�� ���� ������ ���ε� �� ����
        bulletRb.velocity = targetDirection * playerProjectileSpeed;
    }
    public void PlayerHit(float damageAmount)
    {
        playerCurHp -= damageAmount;

        Debug.Log($"Player Hit Cur HP : {playerCurHp}");

        if (playerCurHp <= 0)
        {
            IsDeath = true;
            Die();
        }
    }

    public void Reward(int exp)
    {
        playerCurExp += exp;

        if (playerCurExp >= playerMaxExp)
        {
            LevelyUp();
            playerCurExp = 0;
        }
    }
    protected override void Die()
    {
        Debug.LogError("Player Die");
        StageManager.Instance.PlayerDeath();
    }

    public void LevelyUp()
    {
        // ��ũ���ͺ� ������Ʈ�� ���� ���ο� ������ �޾ƿ� player setting
        // InitPlayer();

        playerLv++;
    }
}
