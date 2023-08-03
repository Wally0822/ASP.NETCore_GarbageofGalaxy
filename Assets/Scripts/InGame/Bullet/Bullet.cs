using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float bulletLifeTime;
    [SerializeField] public float rangedBulletDamage;
    [SerializeField] public float meleeBulletDamage;
    [SerializeField] public float playerBulletDamage;
    
    [SerializeField] GameObject player;
    [SerializeField] GameObject bossMonster;
    [SerializeField] GameObject meleeMonster;
    [SerializeField] GameObject rangedMonster;
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
            bulletRb = rb;
        else
            bulletRb = gameObject.AddComponent<Rigidbody2D>();

        if (gameObject.TryGetComponent<CircleCollider2D>(out CircleCollider2D collider2D))
            bulletCollider = collider2D;
        else
            bulletCollider = gameObject.AddComponent<CircleCollider2D>();

        bulletRb.gravityScale = 0;
        bulletCollider.isTrigger = true;

        bulletSpawner = player.transform;

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
            // �÷��̾� �浹 ó��
            Player hitPlayer = other.gameObject.GetComponent<Player>();
            //hitPlayer.PlayerHit()
        }
        // Monster Hit
        else if (other.gameObject.tag == "Monster" && setShooter.name == "Player")
        {
            Debug.LogError("Monster Hit");
            // ���� �浹 ó��
            other.gameObject.SetActive(false);
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
