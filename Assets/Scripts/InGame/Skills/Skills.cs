using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public string skillName;
    public int curSkillLevel;
    public int maxSkillLevel;
    public int requiredUnlockSkillLevel;

    public Skills(EnumTypes.PlayerSkiilsType skillName, int maxLv, int reqLb)
    {
        this.skillName = skillName.ToString();
        curSkillLevel = 0;
        maxSkillLevel = maxLv;
        requiredUnlockSkillLevel = reqLb;
    }

    // ��ų �ع� ���� ��ȯ
    public virtual bool CanSkillOpen()
    {
        return (curSkillLevel <= maxSkillLevel) && curSkillLevel > 0;
    }

    public void LevelUp()
    {
        if (CanSkillOpen())
            curSkillLevel++;
    }
}
