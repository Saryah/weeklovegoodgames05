using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string projectileTag;
    public float speed = 10f;
    public float maxDistance = 20f;
    public int damage = 10;

    private Vector3 startPosition;
    public Vector3 targetPosition;
    private Rigidbody rb;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();

        // Get the direction from fire point to target
        Vector3 direction = (targetPosition - startPosition).normalized;

        // Set velocity in that direction
        rb.linearVelocity = direction * speed;
    }

    void FixedUpdate()
    {
        if (GameManager.instance.gamePaused || GameManager.instance.gameOver)
            return;
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (projectileTag == "Enemy")
        {
            if (other.gameObject.tag == "Player")
            {
                Player.instance.TakeDamage(damage);
            }

            if (other.gameObject.tag == "NPC")
            {
                other.gameObject.GetComponent<NpcAI>().TakeDamage(damage);
            }

            if (other.gameObject.tag == "Obstical")
            {
                other.gameObject.GetComponent<Object>().TakeDamage(damage);
            }
        }

        if (projectileTag == "Player")
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<AlienAI>().TakeDamage(damage);
            }
        }

        if (projectileTag == "Obstical")
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<AlienAI>().TakeDamage(damage);
            }
        }

        if (projectileTag == "NPC")
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<AlienAI>().TakeDamage(damage);
            }
        }
        // Do a thing when hitting something
        Debug.Log("Hit: " + other.gameObject.name);
        
        Destroy(gameObject);
    }
}
