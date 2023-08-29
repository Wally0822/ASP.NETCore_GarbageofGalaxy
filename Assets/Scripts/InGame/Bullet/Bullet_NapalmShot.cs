using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_NapalmShot : MonoBehaviour
{
    // Bullet info
    private Rigidbody2D bulletRb;
    private CircleCollider2D bulletCollider2D;

    // child info
    private GameObject napalmobj;
    private Skill_NapalmShotChild skill_NapalmShotChild;
    private Vector3 maxScale;
    private float lifeTime;

    #region Unity Life Cycle
    private void Awake()
    {
        NapalmShotInit();
    }

    private void OnEnable()
    {
        Destroy(gameObject, 6f);
    }

    #endregion

    private void NapalmShotInit()
    {
        if (gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody2D))
        {
            bulletRb = rigidbody2D;
        }
        else
        {
            bulletRb = gameObject.AddComponent<Rigidbody2D>();
        }

        if (gameObject.TryGetComponent<CircleCollider2D>(out CircleCollider2D circleCollider2D))
        {
            bulletCollider2D = circleCollider2D;
        }
        else
        {
            gameObject.AddComponent<CircleCollider2D>();
        }

        napalmobj = gameObject.transform.GetChild(0).gameObject;

        if (napalmobj.TryGetComponent<Skill_NapalmShotChild>(out Skill_NapalmShotChild napalmShotChildChild))
        {
            skill_NapalmShotChild = napalmShotChildChild;
        }
        else
        {
            skill_NapalmShotChild = napalmobj.AddComponent<Skill_NapalmShotChild>();
        }

        maxScale = Vector3.one;
        lifeTime = 4f;

        napalmobj.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            if (collision.gameObject.name == "BossOne")
            {
                // ���� ���� �߰� �ʿ�
            }
            else if (collision.gameObject.name == "BasicMeleeMonster")
            {
                bulletRb.velocity = Vector3.zero;
                skill_NapalmShotChild.SetNapalmShotScale(maxScale);
                napalmobj.SetActive(true);
                Destroy(gameObject, lifeTime);
            }
            else
            {
                // �ٸ� ���� ���� �߰� ��
            }
        }
        else if (collision.gameObject.tag == "Wall")
        {
            // Destroy(gameObject);
        }
        else
        {
            // �ٸ� ������Ʈ �߰� ��
        }
    }

    public void SetNapalmShotScale(int skillLevel)
    {
        maxScale *= 1 + (skillLevel * 0.2f);
    }
}
