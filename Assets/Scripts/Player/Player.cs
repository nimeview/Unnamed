using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float attackRange = 1.5f;
    public int attackDamage = 20;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;

    public float attackAngle = 45f;

    private Vector2 movement;
    private Rigidbody2D rb;

    public Action OnAttack;
    public Action<int> OnTakeDamage;
    public Action OnDie;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        OnAttack += Attack;
        OnTakeDamage += TakeDamage;
        OnDie += Die;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextAttackTime)
        {
            OnAttack?.Invoke();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                if (IsEnemyInFront(enemy.transform))
                {
                    enemy.GetComponent<Enemy>().OnTakeDamage?.Invoke(attackDamage);
                }
            }
        }
    }

    bool IsEnemyInFront(Transform enemy)
    {
        Vector2 directionToEnemy = (enemy.position - transform.position).normalized;
        float angleToEnemy = Vector2.Angle(transform.right, directionToEnemy);
        return angleToEnemy <= attackAngle;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            OnDie?.Invoke();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}