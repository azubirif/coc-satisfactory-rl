using LemEngine;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody2D rb;
    public LayerMask enemyMask;
    public float detectionRange = 0.5f;
    public float damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCollision();
    }

    private void CheckCollision()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, detectionRange, enemyMask);
        if (col != null)
        {
            col.gameObject.GetComponent<IAttackable>().RecieveDamage(damage);
            Destroy(gameObject);
        }
    }
}
