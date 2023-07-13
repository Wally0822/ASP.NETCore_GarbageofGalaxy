using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public override void  Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");

        Vector3 moveDir = new Vector3(horizontalInput, 0, verticalInput).normalized;
        playerRb.velocity = moveDir * playerSpeed;

        // ���� �信 ���̴� ���콺 ��ġ�� ������
        Vector3 mousePosition = Input.mousePosition;

        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            // �÷��̾��� ��ġ�� ���콺 ��ġ ���� ���͸� ����Ͽ� �÷��̾ ȸ����Ŵ
            Vector3 targetDirection = hit.point - transform.position;
            targetDirection.y = 0f; // y �� ȸ�� ����

            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                playerRb.MoveRotation(targetRotation);
            }
        }
    }
}
