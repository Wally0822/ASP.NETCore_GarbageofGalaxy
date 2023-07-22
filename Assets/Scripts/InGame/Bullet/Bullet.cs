using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject player;
    Transform bulletSpawner;
    Rigidbody bulletRb;
    short lifeTime = 5;
    Vector3 dirBullet;

    private void Awake()
    {
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    private void BulletInit()
    {
        if (gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
            bulletRb = rb;
        else
            bulletRb = gameObject.AddComponent<Rigidbody>();

        bulletSpawner = player.transform.GetChild(0);

        bulletSpeed = 500f;

        // ���콺 ���⿡ ���� ���� ���� ���
        DirBullet();
    }

    private void DirBullet()
    {
        // ���� ���� ���콺 ����Ʈ
        Vector3 mousePosition = Input.mousePosition;
        // ���콺 ��ǥ�� ������ǥ �������� ��ȯ
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            // ���⺤�� ���ϱ�
            dirBullet = (hit.point - bulletSpawner.position).normalized;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� �浹 üũ & �浹 �� ����
        if (other.tag == "Monster")
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
