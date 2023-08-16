using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : MonsterBase
{
    // temp monster status
    [SerializeField] private int meleeMonster_Level;
    [SerializeField] private int meleeMonster_exp;
    [SerializeField] private float meleeMonster_Hp;
    [SerializeField] private float meleeMonster_CurHp;
    [SerializeField] private float meleeMonster_Speed;
    [SerializeField] private float meleeMonster_RateOfFire;
    [SerializeField] private float meleeMonster_ProjectileSpeed;
    [SerializeField] private float meleeMonster_CollisionDamage;
    [SerializeField] private int meleeMonster_Score;
    [SerializeField] private float meleeMonster_Range;
    [SerializeField] private bool isMeleeMonsterDead;


    #region unity event func

    protected override void Awake()
    {
        base.Awake();
        isMeleeMonsterDead = false;
    }

    // stage ���濡 ���� Level�� �ɷ�ġ �ο� => ���� ���� �޾ƿ���
    protected override void OnEnable()
    {
        base.OnEnable();

        if (isMeleeMonsterDead == true)
        {
            SetMeleeMonsterStatus(stageNum);
        }
        // init melee monster variable
    }

    protected override void Start()
    {
        InGameManager.Instance.AddActionType(EnumTypes.InGameParamType.Monster, EnumTypes.StageStateType.Awake, GetMeleeMonsterInfo);
        base.Start();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        isMeleeMonsterDead = true;
    }
    #endregion
    protected override void SetMonsterName()
    {
        MonsterName = "BasicMeleeMonster";
    }

    protected void SetMeleeMonsterStatus(int inputStageNum)
    {
        //Debug.LogError("SetMeleeMonsterStatus : " + inputStageNum);

        meleeMonster_Level = meleeMonsterStatus[inputStageNum].level;
        meleeMonster_exp = meleeMonsterStatus[inputStageNum].exp;
        meleeMonster_Hp = meleeMonsterStatus[inputStageNum].hp;
        meleeMonster_CurHp = meleeMonster_Hp;
        meleeMonster_Speed = meleeMonsterStatus[inputStageNum].speed;
        meleeMonster_RateOfFire = meleeMonsterStatus[inputStageNum].rate_of_fire;
        meleeMonster_ProjectileSpeed = meleeMonsterStatus[inputStageNum].projectile_speed;
        meleeMonster_CollisionDamage = meleeMonsterStatus[inputStageNum].collision_damage;
        meleeMonster_Score = meleeMonsterStatus[inputStageNum].score;
        meleeMonster_Range = meleeMonsterStatus[inputStageNum].ranged;
    }

    protected override void MonsterStatusUpdate()
    {
        //  Debug.LogError("MonsterStatusUpdate : " + stageNum);
        SetMeleeMonsterStatus(stageNum);
    }


    public override void Attack()
    {
        transform.Rotate(0, 0, 30);
        // �ӽ�
        PlayerHit();
    }

    public override void Hit()
    {
        meleeMonster_CurHp -= player.playerAttackPower;

        if (meleeMonster_CurHp <= 0)
        {
            player.Reward(meleeMonster_exp);
            MonsterDeath();
        }
    }

    public void PlayerHit()
    {
        player.PlayerHit(meleeMonster_CollisionDamage);
    }

    protected override IEnumerator State_Move()
    {
        // ���� ���� �� �̵��ӵ� �� ���� ���� �߰�
        return base.State_Move();
    }
}