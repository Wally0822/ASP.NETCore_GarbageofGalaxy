using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonster : MonsterBase
{
    private Bullet bullet;
    private Rigidbody2D bulletRb;

    // temp monster status
    [SerializeField] protected int rangedMonster_Level;
    [SerializeField] protected int rangedMonster_exp;
    [SerializeField] protected float rangedMonster_Hp;
    [SerializeField] protected float rangedMonster_CurHp;
    [SerializeField] protected float rangedMonster_Speed;
    [SerializeField] protected float rangedMonster_RateOfFire;
    [SerializeField] protected float rangedMonster_ProjectileSpeed;
    [SerializeField] protected float rangedMonster_CollisionDamage;
    [SerializeField] protected int rangedMonster_Score;
    [SerializeField] protected float rangedMonster_Range;

    #region unity event func
    // stage ���濡 ���� Level�� �ɷ�ġ �ο� => ���� ���� �޾ƿ���
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();
        SetRangedMonsterStatus();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void SetMonsterName()
    {
        MonsterName = "RangedMonster";
    }

    private void SetRangedMonsterStatus()
    {
        rangedMonster_Level = rangedMonsterStatus[0].level;
        rangedMonster_exp = rangedMonsterStatus[0].exp;
        rangedMonster_Hp = rangedMonsterStatus[0].hp;
        rangedMonster_CurHp = rangedMonster_Hp;
        rangedMonster_Speed = rangedMonsterStatus[0].speed;
        rangedMonster_RateOfFire = rangedMonsterStatus[0].rate_of_fire;
        rangedMonster_ProjectileSpeed = rangedMonsterStatus[0].projectile_speed;
        rangedMonster_CollisionDamage = rangedMonsterStatus[0].collision_damage;
        rangedMonster_Score = rangedMonsterStatus[0].score;
        rangedMonster_Range = rangedMonsterStatus[0].ranged;
    }

    #endregion
    protected override IEnumerator State_Move()
    {
        // ���� ���� �� �̵��ӵ� �� ���� ���� �߰�
        return base.State_Move();
    }

    public override void Attack()
    {
        Debug.LogError("Ranged Monster Attack : " + rangedMonster_CollisionDamage);

        GameObject pullBullet = ObjectPooler.SpawnFromPool("Bullet2D", gameObject.transform.position);
        playerTargetDirection = (player.transform.position - gameObject.transform.position).normalized;

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
        bulletRb.velocity = playerTargetDirection * rangedMonster_ProjectileSpeed;
    }

    public override void Hit()
    {
        rangedMonster_CurHp -= player.playerAttackPower;

        if (rangedMonster_CurHp <= 0)
        {
            MonsterDeath();
        }
    }

    public void PlayerHit()
    {
        player.PlayerHit(rangedMonster_CollisionDamage);
    }
}
