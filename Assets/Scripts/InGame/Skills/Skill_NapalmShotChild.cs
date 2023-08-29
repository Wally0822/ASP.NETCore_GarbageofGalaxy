using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_NapalmShotChild : MonoBehaviour
{
    private CircleCollider2D napalmCollider;
    private MeleeMonster meleeMonster;

    private float rotationSpeed;
    private Vector3 origineScale;
    private bool isHitMonsterRunning;
    private bool isColliding;
    private IEnumerator hitMonsterCoroutine;
    private WaitForSeconds repeatTime;



    private void Awake()
    {
        if (gameObject.TryGetComponent<CircleCollider2D>(out CircleCollider2D circleCollider2D))
        {
            napalmCollider = circleCollider2D;
        }
        else
        {
            napalmCollider = gameObject.AddComponent<CircleCollider2D>();
        }
    }

    private void OnEnable()
    {
        MonsterRotate();
        isHitMonsterRunning = false; // OnEnable���� �ʱ�ȭ
        hitMonsterCoroutine = null; // �ڷ�ƾ �ʱ�ȭ
    }

    private void Start()
    {
        origineScale = Vector3.one;
        rotationSpeed = 20f;
        repeatTime = new WaitForSeconds(1f);
        isHitMonsterRunning = false;
        isColliding = false;
    }

    public void SetNapalmShotScale(Vector3 maxScale)
    {
        gameObject.transform.localScale = maxScale;

        napalmCollider.enabled = false;
        napalmCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            isColliding = true;

            if (collision.gameObject.name == "BasicMeleeMonster")
            {
                if (collision.gameObject.TryGetComponent<MeleeMonster>(out MeleeMonster meleeMonster))
                {
                    this.meleeMonster = meleeMonster;
                }
                else
                {
                    this.meleeMonster = collision.gameObject.AddComponent<MeleeMonster>();
                }

                // HitMonster �ڷ�ƾ ���� ���� üũ
                if (!isHitMonsterRunning)
                {
                    hitMonsterCoroutine = HitMonster(collision.gameObject); // �ڷ�ƾ �Ҵ�
                    StartCoroutine(HitMonster(collision.gameObject));
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            isColliding = false;
        }
    }

    private IEnumerator MonsterRotate()
    {
        while (true)
        {
            // ȸ�� ���� ��� (������ �ӵ��� ����Ͽ� ȸ��)
            float rotationAmount = rotationSpeed * Time.deltaTime;

            // ������Ʈ�� ȸ����Ŵ
            gameObject.transform.Rotate(Vector3.back, rotationAmount);

            yield return null;
        }
    }

    private IEnumerator HitMonster(GameObject monster)
    {
        isHitMonsterRunning = true;

        while (monster != null)
        {
            if (meleeMonster != null)
            {
                meleeMonster.Hit();
            }

            yield return repeatTime;
        }

        isHitMonsterRunning = false; 
        hitMonsterCoroutine = null; // �ڷ�ƾ ���� �� ���� �ʱ�ȭ
    }
}
