using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static EnumTypes;

/// <summary>
/// ȯ�� ������ �ݹ� �Լ� ��� ����
/// ���� ��� ����  �ݹ� �Լ�
///_parameters[EnumTypes.InGameParamType.Player].InvokeCallBack(EnumTypes.PlayerSkiils.DoubleShot);
/// </summary>
public class InGameManager : MonoSingleton<InGameManager>
{
    private Dictionary<EnumTypes.InGameParamType, InGameParamBase> _parameters = new();

    private void Awake()
    {
        RegisterParams();
    }

    private void RegisterParams()
    {
        _parameters.Add(EnumTypes.InGameParamType.Player, new InGamePlayerParams());
    }
    public void InvokeCallBacks(EnumTypes.InGameParamType type, int callBackIndex)
    {
        InGameParamBase param = null;

        switch (type)
        {
            case EnumTypes.InGameParamType.Player:
                if (_parameters.TryGetValue(type, out param) == false)
                {
                    return;
                }
                InGamePlayerParams players = param as InGamePlayerParams;
                players.InvokeCallBack(callBackIndex);
                break;
        }
    }
    /// <summary>
    /// ��뿹��
    /// ex) ���� ����, �ݹ� �Լ� �̸�, �ݹ� �Լ� 
    ///     AddActionType(EnumTypes.InGameParamType.Player, PlayerSkiils.DoubleShot, () => Debug.Log("����"));
    /// </summary>
    /// <typeparam name="TEnum">EnumŸ��</typeparam>
    /// <param name="param">���� ����</param>
    /// <param name="actionType">�ݹ� �Լ� Ÿ��</param>
    /// <param name="action">�ݹ� �Լ�</param>
    public void AddActionType<TEnum>(EnumTypes.InGameParamType param, TEnum actionType, UnityAction action)
    {
        int idx = GetEnumNumber(actionType);
        _parameters[param].AddCallBack(idx, action);
    }
    public void AddActionType<TEnum>(EnumTypes.InGameParamType param, int idx, UnityAction action)
    {
        _parameters[param].AddCallBack(idx, action);
    }
}
