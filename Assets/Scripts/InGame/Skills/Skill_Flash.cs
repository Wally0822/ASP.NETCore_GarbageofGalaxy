using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Flash : SkillBase
{
    public override void SkillCoolTime()
    {
        //��ų ��Ÿ��
        _coolTime = 10f;
    }

    public override void SkillLevelUp()
    {
        //��ų �������� ȿ��
        _skillLevel++;
    }

    public override void SkillShot()
    {
        //���� ��� ����
        Debug.Log("����");
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
