using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int currentHealth = 100;
    public int maxHealth = 100;

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
    }
}
