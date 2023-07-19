using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ParentSkillNode : Skills
{
    public List<ChildrenSkillNode> children;

    // �Է¹��� �Ű� ������ �������� �θ� Ŭ������ ������ �ʱ�ȭ
    public ParentSkillNode(EnumTypes.PlayerSkiils skiils, int maxLv, int reqLv) : base(skiils, maxLv, reqLv)
    {
        children = new List<ChildrenSkillNode>();
    }
}

public partial class ChildrenSkillNode : Skills
{
    public ParentSkillNode parent;

    // �θ� Ŭ���� ������ �ʱ�ȭ �� ParentSkillNode ����
    public ChildrenSkillNode(EnumTypes.PlayerSkiils skiils, int maxLv, int reqLv, ParentSkillNode parentNode) : base(skiils, maxLv, reqLv)
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