using UnityEngine;

public class Object : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int cost;
    public Sprite icon;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
