using UnityEngine;

public class Projectile : MonoBehaviour
{
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
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // Do a thing when hitting something
        Debug.Log("Hit: " + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            Player.instance.TakeDamage(damage);
            
        }

        if (other.gameObject.tag == "NPC")
        {
            
        }

        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<AlienAI>().TakeDamage(damage);
        }

        if (other.gameObject.tag == "Obstical")
        {
            other.gameObject.GetComponent<Object>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
