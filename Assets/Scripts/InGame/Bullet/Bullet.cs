using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject player;
    Transform bulletSpawner;
    Rigidbody2D bulletRb; 
    float lifeTime = 5f;
    Vector2 dirBullet;

    private void BulletInit()
    {
        if (gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            bulletRb = rb;
        else
            bulletRb = gameObject.AddComponent<Rigidbody2D>(); 

        bulletRb.gravityScale = 0;

        bulletSpawner = player.transform;

        bulletSpeed = 10f; 

        // ���콺 ���⿡ ���� ���� ���� ���
        DirBullet();
    }

    private void DirBullet()
    {
        // ���� ���� ���콺 ����Ʈ
        Vector3 mousePosition = Input.mousePosition;
        // ���콺 ��ǥ�� ������ǥ �������� ��ȯ
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 bulletSpawnerPosition = bulletSpawner.position;

        // ���⺤�� ���ϱ� (2D ������ ������ ���ϱ� ���� Z��ǥ�� ����)
        dirBullet = ((Vector2)mouseWorldPosition - bulletSpawnerPosition).normalized;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        // ���� �浹 üũ & �浹 �� ����
        if (other.CompareTag("Monster")) 
        {
            // ���Ϳ� �浹 �� Bullet ��ȯ
            ReturnBullet();
        }
    }

    private void ReturnBullet() => gameObject.SetActive(false);

    private void OnEnable()
    {
        BulletInit();

        bulletRb.velocity = dirBullet * bulletSpeed;

        // �߻� �� 5�� �� Bullet ��Ȱ��ȭ
        Invoke("ReturnBullet", lifeTime);
    }

    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();
    }
}
