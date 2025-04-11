using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 20f;
    public int damage = 10;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Move forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check distance traveled
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

        if (other.gameObject.tag == "Alien")
        {
            
        }

        if (other.gameObject.tag == "Obstical")
        {
            
        }
        // Add your logic here (e.g., damage, effects, etc.)

        Destroy(gameObject); // Destroy on hit
    }
}
