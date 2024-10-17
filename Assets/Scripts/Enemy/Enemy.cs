using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;

    private Transform player;
    public float chaseRange = 5f;
    public float attackAngle = 45f;

    public Action OnAttackPlayer;
    public Action<int> OnTakeDamage;
    public Action OnDie;

    private void Start()
    {
        currentHealth = maxHealth;

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found! Ensure there is an object with the 'Player' tag in the scene.");
        }

        OnAttackPlayer += AttackPlayer;
        OnTakeDamage += TakeDamage;
        OnDie += Die;
    }

    private void Update()
    {
        if (player == null) return;

        if (Vector2.Distance(transform.position, player.position) <= chaseRange)
        {
            ChasePlayer();

            if (Vector2.Distance(transform.position, player.position) <= attackRange && Time.time >= nextAttackTime)
            {
                if (IsPlayerInFront())
                {
                    OnAttackPlayer?.Invoke();
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, 2f * Time.deltaTime);
    }

    bool IsPlayerInFront()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer);
        return angleToPlayer <= attackAngle;
    }

    void AttackPlayer()
    {
        if (player != null)
        {
            Player playerScript = player.GetComponent<Player>();
            playerScript.OnTakeDamage?.Invoke(attackDamage);
        }
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
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}