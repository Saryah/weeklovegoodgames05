using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpaceShip : MonoBehaviour
{
    public int numberOfSpawns;
    public GameObject[] aliens;
    public Transform spawnPoint;
    [SerializeField] Transform origin;
    public Transform destination;
    public float dropTimer;
    public float dropCountdown;
    public float speed;
    [SerializeField] Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dropCountdown = dropTimer;
        numberOfSpawns = (int)(GameManager.instance.level * 1.5f);
        GameObject[] possibleSpots = GameObject.FindGameObjectsWithTag("Ground");
        Debug.Log("Landing Spots Found " + possibleSpots.Length);
        int randDrop = Random.Range(0, possibleSpots.Length);
        destination = possibleSpots[randDrop].transform;
    }

    void Update()
    {
        if (numberOfSpawns >= 0)
        {
            Vector2 currentXZ = new Vector2(transform.position.x, transform.position.z);
            Vector2 destinationXZ = new Vector2(destination.position.x, destination.position.z);
            float distanceXZ = Vector2.Distance(currentXZ, destinationXZ);

            if (distanceXZ < 0.1f)
            {
                Drop();
            }
            else
            {
                Move();
            }
        }

        if (numberOfSpawns <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    void Move()
    {
        // Create a target position with the same Y as the current position
        Vector3 targetPosition = new Vector3(destination.position.x, transform.position.y, destination.position.z);

        Vector3 direction = (targetPosition - transform.position).normalized;
        Vector3 movePosition = transform.position + direction * speed * Time.deltaTime;
        rb.MovePosition(movePosition);
    }
    void Drop()
    {
        if (dropCountdown <= 0)
        {
            int enemyToSpawn = Random.Range(0, aliens.Length);
            Debug.Log("Dropped Alien");
            Instantiate(aliens[enemyToSpawn], spawnPoint.position, Quaternion.identity);
            numberOfSpawns--;
            dropCountdown = dropTimer;
        }
        else
        {
            dropCountdown -= Time.deltaTime;
        }
        
    }

    public void OriginPoint(Transform _origin)
    {
        origin = _origin;
    }
}
