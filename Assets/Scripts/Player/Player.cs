using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int maxMana = 50;
    public int currentMana;
    public float attackRange = 1.5f;
    public int physicalDamage = 20;
    public int magicalDamage = 10;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;
    public string damageType = "Physical";

    public float attackAngle = 45f;
    public float physicalResistance = 0.2f;
    public float magicalResistance = 0.1f;

    private Vector2 movement;
    private Rigidbody2D rb;

    public Action OnAttack;
    public Action<int, string> OnTakeDamage;
    public Action OnDie;

    private void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        rb = GetComponent<Rigidbody2D>();

        OnAttack += Attack;
        OnTakeDamage += TakeDamage;
        OnDie += Die;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextAttackTime && currentMana >= 10)
        {
            OnAttack?.Invoke();
            nextAttackTime = Time.time + attackCooldown;
            currentMana -= 10;
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
                    if(damageType == "Physical")
                    {
                        enemy.GetComponent<Enemy>().OnTakeDamage?.Invoke(physicalDamage, damageType);
                    }
                    else
                    {
                        enemy.GetComponent<Enemy>().OnTakeDamage?.Invoke(magicalDamage, damageType);
                    }
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

    public void TakeDamage(int damage, string damageType)
    {
        if (damageType == "Physical")
        {
            damage = Mathf.RoundToInt(damage * (1 - physicalResistance));
        }
        else if (damageType == "Magical")
        {
            damage = Mathf.RoundToInt(damage * (1 - magicalResistance));
        }

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