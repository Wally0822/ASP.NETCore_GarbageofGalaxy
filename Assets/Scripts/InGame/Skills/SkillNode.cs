using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ParentSkillNode : Skills
{
    public List<ChildrenSkillNode> children;

    // �Է¹��� �Ű� ������ �������� �θ� Ŭ������ ������ �ʱ�ȭ
    public ParentSkillNode(EnumTypes.PlayerSkiilsType skiils, int maxLv, int reqLb) : base(skiils, maxLv, reqLb)
    {
        children = new List<ChildrenSkillNode>();
    }
}

public partial class ChildrenSkillNode : Skills
{
    public ParentSkillNode parent;

    // �θ� Ŭ���� ������ �ʱ�ȭ �� ParentSkillNode ����
    public ChildrenSkillNode(EnumTypes.PlayerSkiilsType skiils, int maxLv, int reqLb, ParentSkillNode parentNode) : base(skiils, maxLv, reqLb)
    {
        parent = parentNode;
    }

    // ��ų �ر� ���� �Ǵ�
    public override bool CanSkillOpen()
    {
        // �θ� ��忡�� ���ǵ� �ر����� (ex �� ��ų�� �θ� ��忡 ���ǵ� �ر� ���� ����)
        return parent.curSkillLevel >= requiredUnlockSkillLevel && base.CanSkillOpen();
    }
}