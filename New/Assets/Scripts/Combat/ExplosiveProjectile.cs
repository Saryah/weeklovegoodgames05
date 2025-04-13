using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 20f;
    public int damage = 10;
    public float explosionRadius = 5f;
    public float delayBeforeLaunch = 1f; // Set this to match animation length
    public GameObject explosionEffectPrefab;

    private Vector3 startPosition;
    public Vector3 targetPosition;
    private Rigidbody rb;
    private bool isLaunched = false;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();

        // Begin delay coroutine
        StartCoroutine(DelayedLaunch());
    }

    private System.Collections.IEnumerator DelayedLaunch()
    {
        
        yield return new WaitForSeconds(delayBeforeLaunch);

        Vector3 direction = (targetPosition - startPosition).normalized;
        rb.linearVelocity = direction * speed;
        isLaunched = true;
        
    }

    void FixedUpdate()
    {
        if (!isLaunched) return;

        float distanceTraveled = Vector3.Distance(startPosition, transform.position);
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isLaunched) return;
        Vector3 explosion = other.collider.gameObject.transform.position;
        Explode(explosion);
    }

    void Explode(Vector3 explosion)
    {
        // Show explosion effect
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, explosion, Quaternion.identity);
        }

        // Damage all objects in range
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearby in colliders)
        {
            GameObject obj = nearby.gameObject;

            if (obj.CompareTag("Player"))
            {
                Player.instance.TakeDamage(damage);
            }
            else if (obj.CompareTag("Enemy"))
            {
                obj.GetComponent<AlienAI>()?.TakeDamage(damage);
            }
            else if (obj.CompareTag("NPC"))
            {
                // Add NPC logic here
            }
            else if (obj.CompareTag("Obstical"))
            {
                obj.GetComponent<Object>()?.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the explosion radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

