using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumTypes;

public class BossOne : BossBase
{
    [SerializeField] private float _delayTime = 2f;
    [SerializeField] private List<float> _rad;
    [SerializeField] private List<float> _initDegree;
    [SerializeField] private float lotationSpeed = 0.01f;
    [SerializeField] private float lotationSpeed2 = 0.1f;
    [Range(0, 100)]
    [SerializeField] private float tempHP = 100;
    [Header("BossProjectile")]
    [SerializeField] private string projectileName;
    protected override void Start()
    {
        //SetRangedMonsterStatus(stageNum);
        SetBossMonsterStatus(1); //���� �׽�Ʈ
        //ȸ�� �ʱⰪ
        foreach (var pos in _spawners)
        {
            Vector2 tempVector = _boss.transform.position - pos.position;
            _rad.Add(Vector3.Distance(pos.transform.position, _boss.transform.position));
            _initDegree.Add(Mathf.Atan2(tempVector.x, tempVector.y));
        }
        _monsterInfo.rate_of_fire = 5f;
        TransferState(MonsterStateType.Move);
        // StartCoroutine(Co_BossPattern());
    }

    IEnumerator Co_BossPattern()
    {
        yield return new WaitForSeconds(_delayTime);

        TransferState(MonsterStateType.Phase1);
        yield return new WaitUntil(() => _monsterInfo.curHp < _monsterInfo.hp * 0.5f);

        TransferState(MonsterStateType.Phase2);
        yield return new WaitUntil(() => tempHP < _monsterInfo.hp * 0.3f);

        TransferState(MonsterStateType.Phase3);
        yield return new WaitUntil(() => tempHP == 0);

        StartCoroutine(Co_BossDie());
    }

    public void GetDamage(int damage)
    {
        // curHP : �������� �޾ƿ���
        // curHP -= damage;
    }
    public override void Attack()
    {
        ShootProjectile(projectileName);
    }
    private void RotateAttack()
    {
        for (int i = 0; i < _spawners.Count; i++)
        {
            StartCoroutine(Co_halfMoveSpawner(_spawners[i].gameObject, _initDegree[i], _rad[i]));
        }
    }
    IEnumerator Co_halfMoveSpawner(GameObject spawner, float initDegree, float rad)
    {
        float i = 0;
        float transedDegree = initDegree;
        while (true)
        {
            if (i > 360)
            {
                i = 0;
                transedDegree -= 360;
            }
            yield return null;
            Vector3 newPos = Vector3.zero;
            newPos.x = _boss.transform.position.x + rad * Mathf.Cos(transedDegree);
            newPos.y = _boss.transform.position.y + rad * Mathf.Sin(transedDegree);

            spawner.transform.position = newPos;
            transedDegree += lotationSpeed;
            i += lotationSpeed;
        }
    }
    protected override IEnumerator State_Attack()
    {
        // ���� ������ �����϶� ���ѹݺ�
        while (state == MonsterStateType.Attack)
        {
            // ���ݰ��ɻ������� üũ
            // �������̸� Move State�� �̵� 
            Attack();
            yield return new WaitForSeconds(_monsterInfo.rate_of_fire); // 3f ��� monsterData.RateOfFire ��� ���� ��ü
        }
    }
    protected override IEnumerator State_Move()
    {
        // ���� ���� �� �̵��ӵ� �� ���� ���� �߰�
        while (state == MonsterStateType.Move)
        {
            // �÷��̾ ������ ����
            if (player.IsDeath)
            {
                // ���͵��� Ư�� State�� �Ű��ش�
                // TransferState(MonsterStateType.Dance);
                yield break;
            }

            // �÷��̾ ���� ���⺤�͸� ����.
            Vector3 dirVector = (player.transform.position - gameObject.transform.position).normalized;
            //gameObject.transform.LookAt(player.transform.position);

            // ���͸� �ش� �������� ������(�ٸ� ������� �����ص� ����)
            // gameObject.transform.Translate(dirVector * _monsterInfo.MoveSpeed * Time.deltaTime);
            // �ӽ� �̵��ӵ�
            gameObject.transform.Translate(dirVector * 2f * Time.deltaTime);

            // �÷��̾�� �ڱ��ڽ�(����)������ �Ÿ��� ������ ���� ���ɹ����� ���Ͽ� ����(�ٸ� ������� �����ص� ����)
            if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) <= _monsterInfo.ranged)
            {
                StartCoroutine(Co_BossPattern());
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator State_Phase1()
    {
        Debug.Log("������ 1");
        // ���� ������ �����϶� ���ѹݺ�
        while (state == MonsterStateType.Phase1)
        {
            Attack();
            yield return new WaitForSeconds(_monsterInfo.rate_of_fire);
        }
    }
    IEnumerator State_Phase2()
    {
        Debug.Log("������ 2");
        RotateAttack();
        // ���� ������ �����϶� ���ѹݺ�
        while (state == MonsterStateType.Phase2)
        {
            Attack();
            yield return new WaitForSeconds(_monsterInfo.rate_of_fire - 0.4f);
        }
    }
    IEnumerator State_Phase3()
    {
        Debug.Log("������ 3");
        int i = 10;
        // ���� ������ �����϶� ���ѹݺ�
        while (state == MonsterStateType.Phase3)
        {
            Attack();
            yield return new WaitForSeconds(Mathf.Sin(Mathf.Deg2Rad * i + 0.15f));
            // Debug.Log(Mathf.Sin(Mathf.Deg2Rad * i));
            i--;
            if (i < 0) i = 10;
            /*  Attack();
              yield return new WaitForSeconds(0.2f);
              Attack();
              yield return new WaitForSeconds(0.2f);
              Attack();
              yield return new WaitForSeconds(0.5f);

              Attack();
              yield return new WaitForSeconds(0.3f);
              Attack();
              yield return new WaitForSeconds(0.3f);*/
        }
    }

    protected override void SetMonsterName()
    {
        MonsterName = "BossOne";
    }
    IEnumerator Co_BossDie()
    {
        yield return null;
        //���� ��� ��Ÿ���� ����Ʈ �˸� ��� ó��
        Debug.Log("boss die");
        //�������� �Ŵ����� ���� �����˸�
        FindObjectOfType<StageManager>().BossDeath();

    }
    bool _stageOver = false;
    public override void Hit()
    {
        _monsterInfo.curHp -= player.playerAttackPower;
        if (_monsterInfo.curHp <= 0)
        {
            if (_stageOver == true) return;
            _stageOver = true;
            UIManager.Instance.CreateObject<Popup_StageClear>("Popup_StageClear",LayoutType.Middle);
            //player.Reward(_monsterInfo.exp);
        }
    }


    private void SetBossMonsterStatus(int inputStageNum)
    {
        _monsterInfo.level = bossMonsterStatus.level;
        _monsterInfo.exp = bossMonsterStatus.exp;
        _monsterInfo.hp = bossMonsterStatus.hp;
        _monsterInfo.curHp = bossMonsterStatus.hp;
        _monsterInfo.speed = bossMonsterStatus.speed;
        _monsterInfo.rate_of_fire = bossMonsterStatus.rate_of_fire;
        _monsterInfo.projectile_speed = bossMonsterStatus.projectile_speed;
        _monsterInfo.collision_damage = bossMonsterStatus.collision_damage;
        _monsterInfo.score = bossMonsterStatus.score;
        _monsterInfo.ranged = bossMonsterStatus.ranged - 12;
    }

}
