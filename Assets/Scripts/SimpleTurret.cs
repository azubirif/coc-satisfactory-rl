using UnityEngine;
using LemEngine;
using System.Linq;

public class SimpleTurret : MonoBehaviour
{
    public bool drawRange = false;
    public float detectionRange = 5f;
    public LayerMask enemyMask;
    public float shootCooldown = 1f;
    private float currentShootCooldown = 0f;
    public float rotationSpeed = 2f;

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
        currentShootCooldown -= Time.deltaTime;

        Collider2D[] enemiesDetected = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyMask);
        if (enemiesDetected.Length == 0) return;
        Transform closestEnemy = GetClosestEnemy(enemiesDetected);
        RotateToTarget(closestEnemy.transform);

        if (currentShootCooldown <= 0f)
        {
            ShootTarget(closestEnemy.transform);
            currentShootCooldown = shootCooldown;
        }
    }

    private void ShootTarget(Transform target)
    {

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
