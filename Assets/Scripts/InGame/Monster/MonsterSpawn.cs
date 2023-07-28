using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] GameObject monsterPrefab; // ������ ���� ������
    [SerializeField] private int maxMonstersPerSpawnArea = 3; // �� ���� ������ �ִ� ���� ���� ����
    [SerializeField] private float spawnDelay = 2f; // ���� ���� ����
    [SerializeField] private float spawnRadius = 5f; // ���� ���� �ݰ�

    GameObject meleeMonster;
    GameObject rangedMonster;
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
            Instantiate(monsterPrefab, randomPosition, Quaternion.identity);
        }
    }
}
