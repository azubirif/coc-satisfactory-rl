using System.Collections.Generic;
using UnityEngine;
using LemEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleEnemy : MonoBehaviour, IAttackable
{
    public bool drawRange = false;
    public float maxHealth = 25f;
    private float currentHealth;

    public float moveSpeed = 20f;
    public float attackRange = 0.5f;
    public LayerMask objectiveMask;
    public float attackCD = 1.5f;
    private float currentAttackCD = 0f;
    public float attackDMG = 5f;
    private bool canAttack = false;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GoToNearestObjective();
        AttackNearObjective();
    }

    private void GoToNearestObjective()
    {
        Transform nearestObj = GetNearestObjective();
        Vector2 dir = nearestObj.position - transform.position;
        float dist = dir.magnitude;
        if (dist <= attackRange) canAttack = true;
        else canAttack = false;


        Vector2 dirNorm = dir.normalized;

        rb.linearVelocity = dirNorm * moveSpeed * Time.fixedDeltaTime;
    }

    private Transform GetNearestObjective()
    {
        return Utils.GetClosestTransform(transform, Objective.GetObjectiveTransforms());
    }

    private void AttackNearObjective()
    {
        currentAttackCD -= Time.fixedDeltaTime;
        if (!canAttack || currentAttackCD > 0f) return;

        currentAttackCD = attackCD;
        GameObject objGO = Physics2D.OverlapCircle(transform.position, attackRange, objectiveMask).gameObject;
        objGO.GetComponent<IAttackable>().RecieveDamage(attackDMG);
    }

    public float RecieveDamage(float damage)
    {
        float newHealth = currentHealth - damage;
        currentHealth = newHealth;

        if (currentHealth <= 0f) EnemyDie();

        return newHealth;
    }

    private void EnemyDie()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        if (drawRange) Gizmos.DrawSphere(transform.position, attackRange);
    }
}
