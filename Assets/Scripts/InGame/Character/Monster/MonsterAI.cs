using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public EnumTypes.MonsterType monsterType;
    public EnumTypes.FSMMonsterStateType fsmMonsterStateType;

    [Header("Melee Monster")]
    public float meleeMoveSpeed;
    public float meleeAttackCooldown;
    public float meleeDetectionRange; // Ž�� ����
    public float meleeAttackRange; // ���� ����
    public int meleeAttackDamage;

    [Header("Ranged Monster")]
    public float rangedMoveSpeed;
    public float rangedAttackCooldown;
    public float rangedDetectionRange; // Ž�� ����
    public float rangedAttackRange; // ���� ����
    public int rangedAttackDamage;

    [SerializeField] private Transform player;
    [SerializeField] private CircleCollider2D meleeDetectionCollider; // �ٰŸ� ���� Ž�� ������
    [SerializeField] private CircleCollider2D rangedDetectionCollider; // ���Ÿ� ���� Ž�� ������
    [SerializeField] private CircleCollider2D meleeAttackCollider; // �ٰŸ� ���� ���� ������
    [SerializeField] private CircleCollider2D rangedAttackCollider; // ���Ÿ� ���� ���� ������

    float distanceToPlayer;
    [SerializeField] private bool isAttacking = false;
    private bool isAttackingCooldown = false; // ���� �� ��Ÿ�� ���¸� ��Ÿ���� ����
    private float attackCooldownTimer = 0f; // ���� ��Ÿ�� Ÿ�̸�


    private void Awake()
    {
        // ���� ����
        fsmMonsterStateType = EnumTypes.FSMMonsterStateType.Idle;

        // ���Ÿ�
        meleeMoveSpeed = 1f;
        meleeAttackCooldown = 1.5f;
        meleeDetectionRange = 100f;
        meleeAttackRange = 2f;
        meleeAttackDamage = 10;

        // ���Ÿ�
        rangedMoveSpeed = 0.5f;
        rangedAttackCooldown = 2f;
        rangedDetectionRange = 100f;
        rangedAttackRange = 15f;
        rangedAttackDamage = 5;

        // Player���� �Ÿ� ���
        distanceToPlayer = Mathf.Infinity;
    }
    private void Start()
    {
        if (monsterType == EnumTypes.MonsterType.MeleeMonster)
        {
            // melee monster Ž�� ������ SphereCollider
            meleeDetectionCollider = gameObject.AddComponent<CircleCollider2D>();
            meleeDetectionCollider.isTrigger = true;
            meleeDetectionCollider.radius = meleeDetectionRange;

            // melee monster ���� ������ SphereCollider
            meleeAttackCollider = gameObject.AddComponent<CircleCollider2D>();
            meleeAttackCollider.isTrigger = true;
            meleeAttackCollider.radius = meleeAttackRange;
        }
        else if (monsterType == EnumTypes.MonsterType.RangedMonster)
        {
            // ranged monster Ž�� ������ SphereCollider
            rangedDetectionCollider = gameObject.AddComponent<CircleCollider2D>();
            rangedDetectionCollider.isTrigger = true;
            rangedDetectionCollider.radius = rangedDetectionRange;

            // ranged monster ���� ������ SphereCollider
            rangedAttackCollider = gameObject.AddComponent<CircleCollider2D>();
            rangedAttackCollider.isTrigger = true;
            rangedAttackCollider.radius = rangedAttackRange;
        }
    }

    private void Update()
    {
        switch (fsmMonsterStateType)
        {
            case EnumTypes.FSMMonsterStateType.Idle:
                UpdateIdleState();
                break;
            case EnumTypes.FSMMonsterStateType.Chasing:
                UpdateChasingState();
                break;
            case EnumTypes.FSMMonsterStateType.Attacking:
                UpdateAttackingState();
                break;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * GetMoveSpeed() * Time.deltaTime);
    }

    private void UpdateIdleState()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= GetDetectionRange())
        {
            fsmMonsterStateType = EnumTypes.FSMMonsterStateType.Chasing;
        }
    }

    private void UpdateChasingState()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= GetAttackRange())
        {
            fsmMonsterStateType = EnumTypes.FSMMonsterStateType.Attacking;
        }
        else if (distanceToPlayer > GetDetectionRange())
        {
            fsmMonsterStateType = EnumTypes.FSMMonsterStateType.Idle;
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    private void UpdateAttackingState()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (isAttackingCooldown)
        {
            // ��Ÿ�� ���̸� �������� �ʰ� ��Ÿ�� Ÿ�̸Ӹ� ���ҽ�Ŵ
            attackCooldownTimer -= Time.deltaTime;

            if (attackCooldownTimer <= 0f)
            {
                // ��Ÿ���� ������ �ٽ� ���� ���� ���·� ��ȯ
                isAttackingCooldown = false;
            }
        }
        else
        {
            // ��Ÿ���� �ƴ� ���¿����� ���� ����
            if (distanceToPlayer > GetAttackRange())
            {
                fsmMonsterStateType = EnumTypes.FSMMonsterStateType.Chasing;
            }
            else
            {
                Attack();
            }
        }
    }

    private float GetMoveSpeed()
    {
        return monsterType == EnumTypes.MonsterType.MeleeMonster ? meleeMoveSpeed : rangedMoveSpeed;
    }

    private float GetDetectionRange()
    {
        return monsterType == EnumTypes.MonsterType.MeleeMonster ? meleeDetectionRange : rangedDetectionRange;
    }

    private float GetAttackRange()
    {
        return monsterType == EnumTypes.MonsterType.MeleeMonster ? meleeAttackCollider.radius : rangedAttackCollider.radius;
    }

    private float GetAttackCooldown()
    {
        return monsterType == EnumTypes.MonsterType.MeleeMonster ? meleeAttackCooldown : rangedAttackCooldown;
    }

    private void Attack()
    {
        if (!isAttackingCooldown)
        {
            if (monsterType == EnumTypes.MonsterType.MeleeMonster)
            {
                // �ٰŸ� ���Ͱ� ����
                meleeAttackCollider.enabled = true;
            }
            else if (monsterType == EnumTypes.MonsterType.RangedMonster)
            {
                // ���Ÿ� ���Ͱ� ����
                rangedAttackCollider.enabled = true;
            }

            isAttackingCooldown = true;
            attackCooldownTimer = GetAttackCooldown(); // ��Ÿ�� ����
        }
    }

    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� ������ �÷��̾ ������ ����
        if (!isAttacking && distanceToPlayer < GetAttackRange() && other.CompareTag("Player"))
        {
            if (monsterType == EnumTypes.MonsterType.MeleeMonster)
            {
                Debug.Log("Melee Monster Attack");

                other.GetComponent<Player>().PlayerHit(meleeAttackDamage);
            }
            else if (monsterType == EnumTypes.MonsterType.RangedMonster)
            {
                other.GetComponent<Player>().PlayerHit(rangedAttackDamage);
            }

            isAttacking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // ���� �������� �÷��̾ ������ �ٽ� �ݶ��̴� ��Ȱ��ȭ
        if (isAttacking && other.CompareTag("Player"))
        {
            if (monsterType == EnumTypes.MonsterType.MeleeMonster)
            {
                meleeAttackCollider.enabled = false;
            }
            else
            {
                rangedAttackCollider.enabled = false;
            }

            // ���� ���� �� 
            isAttacking = false;
        }
    }
}
