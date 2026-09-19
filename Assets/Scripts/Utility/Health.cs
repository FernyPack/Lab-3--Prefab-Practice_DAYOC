using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBarFill;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
            Die();
    }

    void UpdateHealthUI()
    {
        if (healthBarFill != null)
            healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    void Die()
    {
        if (CompareTag("Enemy") && !hasDied)
        {
            hasDied = true;  
            if (GameManager.Instance != null)
                GameManager.Instance.AddKill();
        }

        Destroy(gameObject);
    }
    private bool hasDied = false;
}
