using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MonsterInfo
{
    public string Name;
    public int Exp;
    public int Score;
    public float Hp;
    public float RateOfFire;
    public MonsterInfo(string name, int exp, float hp, int score, float rateofFire)
    {
        this.Name = name;
        this.Exp = exp;
        this.Hp = hp;
        this.Score = score;
        this.RateOfFire = rateofFire;
    }
}
[CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObjects/Monster Data", order = int.MaxValue)]
public class MonsterData : ScriptableObject
{
    public List<MonsterInfo> MonsterDataList = new List<MonsterInfo>();
    public void InsertMonsterInfo(MonsterInfo info)
    {
        MonsterDataList.Add(info);
    }
    public void InsertMonsterInfo()
    {
        MonsterDataList.Add(new MonsterInfo("BossOne", 100, 101, 102, 0.8f));
    }
    public bool TryGetMonsterInfo(string name, out MonsterInfo monsterInfo)
    {
        foreach (var info in MonsterDataList)
        {
            if (info.Name == name)
            {

                monsterInfo = info;
                return true;
            }
        }
        monsterInfo = null;
        return false;
    }
}
