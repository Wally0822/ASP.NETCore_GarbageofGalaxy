using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    GameObject meleeMonster; // ������ ���� ������
    GameObject rangedMonster; // ������ ���� ������
    private int maxMonstersPerSpawnArea; // �� ���� ������ �ִ� ���� ���� ����
    public float spawnDelay; // ���� ���� ����
    public float spawnRadius; // ���� ���� �ݰ�
    public int meleeMonsterSpawnNum; // ���� ���� ����
    public int rangedMonsterSpawnNum; // ���� ���� ����




    private void Awake()
    {
        maxMonstersPerSpawnArea = 3;
        spawnDelay = 2f;
        spawnRadius = 5f;
        meleeMonsterSpawnNum = 3;
        rangedMonsterSpawnNum = 3;
    }

    private void Start()
    {
        meleeMonster = ObjectPooler.SpawnFromPool("MeleeMonster", gameObject.transform.position);
        rangedMonster = ObjectPooler.SpawnFromPool("RangedMonster", gameObject.transform.position);
        InvokeRepeating("SpawnMonsters", spawnDelay, spawnDelay);
    }

    private void SpawnMonsters(EnumTypes.MonsterType monster)
    {
        // ������ ���� ���� �߿��� ������ ������ ����
        Transform randomSpawnArea = transform.GetChild(Random.Range(0, transform.childCount));

        // �ش� ���� �������� ������ ��ġ�� �����Ͽ� ���͸� ����
        for (int i = 0; i < maxMonstersPerSpawnArea; i++)
        {
            Vector2 randomPosition = (Vector2)randomSpawnArea.position + Random.insideUnitCircle * spawnRadius;

            switch (monster)
            {
                case EnumTypes.MonsterType.MeleeMonster:
                    ObjectPooler.SpawnFromPool("MeleeMonster", randomPosition);
                    break;
                case EnumTypes.MonsterType.RangedMonster:
                    ObjectPooler.SpawnFromPool("RangedMonster", randomPosition);
                    break;
                default:
                    break;
            }
        }
    }
}
