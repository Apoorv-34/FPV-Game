using UnityEngine;
using UnityEngine.UI; // Required for Image
using TMPro; // Required for TextMeshPro

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Professional UI References")]
    public Image healthBarFill; // Drag your 'HealthBarFill' Image here
    public TextMeshProUGUI healthText; // Drag your big '100' number here

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        // Ensure health doesn't drop below 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        // 1. Update the bar (Image.fillAmount works from 0.0 to 1.0)
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }

        // 2. Update the bold number text
        if (healthText != null)
        {
            // "F0" removes decimal points (e.g., 99 instead of 99.4)
            healthText.text = currentHealth.ToString("F0");
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        
        if (GameManager.instance != null) 
        {
            GameManager.instance.PlayerDied();
        }

        var movementScript = GetComponent<PlayerMovement>();
        if(movementScript != null) 
        {
            movementScript.enabled = false;
        }
        
        CharacterController controller = GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }
    }
}