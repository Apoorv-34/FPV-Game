using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI; // Required for Image

public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    [Header("Health Settings")]
    public float health = 50f;
    private float maxHealth;

    [Header("UI Settings")]
    // Change your World Space Slider to a 'Filled' Image and drag it here
    public Image healthBarFill; 

    [Header("Attack Settings")]
    public float damage = 10f;
    public float attackRange = 2f; 
    public float attackRate = 1.5f; 
    private float nextAttackTime = 0f;

    void Start()
    {
        maxHealth = health;
        agent = GetComponent<NavMeshAgent>();
        
        // Initialize Health Bar
        UpdateHealthUI();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) 
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            agent.isStopped = true;

            if (Time.time >= nextAttackTime)
            {
                AttackPlayer();
                nextAttackTime = Time.time + attackRate;
            }
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    void AttackPlayer()
    {
        PlayerHealth pHealth = player.GetComponent<PlayerHealth>();
        if (pHealth != null)
        {
            pHealth.TakeDamage(damage);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        UpdateHealthUI();

        if (health <= 0f) Die();
    }

    void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            // Sets the fill amount between 0.0 and 1.0
            healthBarFill.fillAmount = health / maxHealth;
        }
    }

    void Die()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.AddKill();
        }
        
        Destroy(gameObject);
        Debug.Log("Zombie Died!");
    }
}