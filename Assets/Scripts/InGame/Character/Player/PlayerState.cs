using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Player
{
    public override void Attack()
    {
        if (Input.GetKey(KeyCode.Mouse0))
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

            // �ӵ� ����ġ�� ���� ������ ���ε� �� ���� (5 -> projectileSpeed)
            bulletRb.velocity = targetDirection * 5;
        }
    }
    public void PlayerHit(int damageAmount)
    {
        playerCurHp -= damageAmount;

        Debug.Log($"Player Hit Cur HP : {playerCurHp}");

        if (playerCurHp <= 0)
        {
            Debug.Log("Player Die");
            Die();
        }
    }
}
