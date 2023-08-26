using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_NerfShot : SkillBase
{
    private Sprite Sprite_NerfShot;
    float damageReduction;

    #region Unity Life Cycle

    private void Awake()
    {
        Sprite_NerfShot = Resources.Load<Sprite>("SkillSprites/Skill_NurfShot");
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    #endregion

    public override void SkillCoolTime()
    {
        _coolTime = 5f;
    }

    public override void SkillLevelUp()
    {
        _skillLevel++;
    }

    public override void SkillShot()
    {
        Debug.LogError("NerfShot ");

        GameObject Bullet_NerfShot = ObjectPooler.SpawnFromPool("Bullet2D", _player.transform.position);

        if (Bullet_NerfShot != null)
        {
            Bullet bullet = Bullet_NerfShot.GetComponent<Bullet>();

            // ������ ������ ���
            damageReduction = 1 - (_skillLevel * 0.1f);

            // ��������Ʈ ����
            bullet.GetComponent<SpriteRenderer>().sprite = Sprite_NerfShot; // ��ų�� �´� ���ο� ��������Ʈ

            bullet.bulletDamageReduction = damageReduction;
        }
    }
}
