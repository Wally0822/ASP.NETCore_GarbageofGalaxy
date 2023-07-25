using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginTest : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField_ID;
    [SerializeField] TMP_InputField inputField_PW;
    [SerializeField] Button createIDButton;
    [SerializeField] Button loginButton;

    string input_ID;
    string input_PW;

    private void Start()
    {
        inputField_ID.characterLimit = 12;
    }

    public void OnEndEditID()
    {
        input_ID = inputField_ID.text;
        Debug.Log(input_ID);
    }
    public void OnEndEditPW()
    {
        input_PW = inputField_PW.text;
        Debug.Log(input_PW);
    }

    // ���� ���� ����, �α��� ���� ������ �߰� ����
    public void CreateID()
    {
        input_ID = inputField_ID.text;
        input_PW = inputField_PW.text;
        Debug.Log($"ID ���� �Ϸ� : {input_ID}");
    }

    public void Login()
    {
        input_ID = inputField_ID.text;
        input_PW = inputField_PW.text;

        Debug.Log($"Login �Ϸ� : {input_ID}");
    }


}
