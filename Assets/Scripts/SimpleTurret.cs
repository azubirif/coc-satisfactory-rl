using UnityEngine;
using LemEngine;
using System.Linq;

public class SimpleTurret : MonoBehaviour
{
    public float bulletDMG = 5f;
    public float bulletSpeed = 20f;
    public GameObject bulletPrefab;
    public bool drawRange = false;
    public float detectionRange = 5f;
    public LayerMask enemyMask;
    public float shootCooldown = 1f;
    public float rotationSpeed = 2f;
    public Transform cannonTrans;
    private float currentShootDelay = 0f;

    public bool isActive = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Detectar y disparar enemigos
        TargetAndShoot();
    }

    void OnDrawGizmos()
    {
        if (drawRange) Gizmos.DrawSphere(transform.position, detectionRange);
    }

    void TargetAndShoot()
    {
        if (!isActive) return;

        Collider2D[] enemiesDetected = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyMask);
        if (enemiesDetected.Length == 0) return;
        Transform closestEnemy = GetClosestEnemy(enemiesDetected);
        currentShootDelay -= Time.deltaTime;

        if (currentShootDelay <= 0f)
        {
            ShootTarget(closestEnemy);
        }

        RotateToTarget(closestEnemy.transform);
    }

    private void ShootTarget(Transform target)
    {
        currentShootDelay = shootCooldown;
        GameObject shotBullet = Instantiate(bulletPrefab);
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        shotBullet.transform.position = cannonTrans.position;
        shotBullet.transform.eulerAngles = new Vector3(0, 0, angle);
        shotBullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
        Bullet bullet = shotBullet.GetComponent<Bullet>();
        bullet.damage = bulletDMG;
    }

    private Transform GetClosestEnemy(Collider2D[] enemies)
    {
        int enemiesCount = enemies.Length;
        Transform[] enemiesTrans = new Transform[enemiesCount];

        for (int i = 0; i < enemiesCount; i++)
        {
            enemiesTrans[i] = enemies[i].gameObject.transform;
        }

        return Utils.GetClosestTransform(transform, enemiesTrans);
    }

    private void RotateToTarget(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x)  * Mathf.Rad2Deg - 90f;

        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
