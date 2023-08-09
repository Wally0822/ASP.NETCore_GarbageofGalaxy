using UnityEngine;
using static EnumTypes;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float bulletLifeTime;
    [SerializeField] public float rangedBulletDamage;
    [SerializeField] public float meleeBulletDamage;
    [SerializeField] public float playerBulletDamage;

    [SerializeField] GameObject playerObject;
    [SerializeField] Player player;
    [SerializeField] GameObject bossMonster;
    [SerializeField] MeleeMonster meleeMonster;
    [SerializeField] RangedMonster rangedMonster;
    [SerializeField] GameObject setShooter;

    Vector2 dirBullet;

    private Transform bulletSpawner;
    private CircleCollider2D bulletCollider;
    private Rigidbody2D bulletRb;

    private void Start()
    {
        BulletInit();
    }

    private void BulletInit()
    {
        if (gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            bulletRb = rb;
        }
        else
        {
            bulletRb = gameObject.AddComponent<Rigidbody2D>();
        }

        if (gameObject.TryGetComponent<CircleCollider2D>(out CircleCollider2D collider2D))
        {
            bulletCollider = collider2D;
        }
        else
        {
            bulletCollider = gameObject.AddComponent<CircleCollider2D>();
        }

        bulletRb.gravityScale = 0;
        bulletCollider.isTrigger = true;

        playerObject = FindObjectOfType<Player>().gameObject;

        if (playerObject.TryGetComponent<Player>(out Player player))
        {
            this.player = player;
        }
        else
        {
            player = playerObject.AddComponent<Player>();
        }

        bulletSpawner = playerObject.transform;

        // ������ � ���� �ٲ� (�ʱ� ������ ���� ��ũ���ͺ� ������Ʈ���� ���� �޾ƿ;ߵ�)
        bulletSpeed = 10f;
        bulletLifeTime = 5f;
        rangedBulletDamage = 10f;
        meleeBulletDamage = 20f;
        playerBulletDamage = 10f;
    }


    public void SetShooter(GameObject shooter)
    {
        setShooter = shooter;
    }

    private void ReturnBullet() => gameObject.SetActive(false);

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player Hit
        if (other.gameObject.tag == "Player")
        {
            switch (setShooter.name)
            {
                case "RangedMonster":
                    rangedMonster.PlayerHit();
                    break;
                default:
                    break;
            }
        }
        // Monster Hit
        else if (other.gameObject.tag == "Monster" && setShooter.name == "Player")
        {
            switch (other.gameObject.name)
            {
                case "BasicMeleeMonster":
                    meleeMonster = other.gameObject.GetComponent<MeleeMonster>();
                    meleeMonster.Hit();
                    break;
                case "RangedMonster":
                    rangedMonster = other.gameObject.GetComponent<RangedMonster>();
                    rangedMonster.Hit();
                    break;
                default:
                    break;
            }
            gameObject.SetActive(false);
        }
        else
        {
            // �� or �ٸ� collider �浹ó��
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        // �߻� �� 5�� �� Bullet ��Ȱ��ȭ
        Invoke("ReturnBullet", bulletLifeTime);
    }

    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();
    }
}
