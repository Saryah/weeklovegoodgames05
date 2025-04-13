using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public GameObject[] aliens;
    public Transform spawnPoint;
    [SerializeField] Transform origin;
    public Transform destination;
    public float dropTimer;
    public float dropCountdown;
    public float speed;
    public float verticalSpeed = 2f;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;

    private enum Phase { Approaching, Landing, Dropping, Takeoff, Leaving }
    private Phase currentPhase = Phase.Approaching;

    private int numberOfSpawns;
    private bool doorOpened = false;
    private bool doorClosed = false;
    private bool isOpeningDoor = false;
    private bool isClosingDoor = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dropCountdown = dropTimer;

        // Calculate number of spawns based on level every 5 levels
        int level = GameManager.instance.level;
        int spawnTier = level / 5;
        numberOfSpawns = Mathf.Clamp(spawnTier * 3, 1, 100);

        GameObject[] possibleSpots = GameObject.FindGameObjectsWithTag("Ground");
        int randDrop = Random.Range(0, possibleSpots.Length);
        destination = possibleSpots[randDrop].transform;
    }

    void Update()
    {
        switch (currentPhase)
        {
            case Phase.Approaching:
                HandleApproach();
                break;

            case Phase.Landing:
                HandleLanding();
                break;

            case Phase.Dropping:
                HandleDropping();
                break;

            case Phase.Takeoff:
                HandleTakeoff();
                break;

            case Phase.Leaving:
                HandleLeaving();
                break;
        }
    }

    void HandleApproach()
    {
        Vector3 flatDest = new Vector3(destination.position.x, transform.position.y, destination.position.z);
        float distance = Vector3.Distance(transform.position, flatDest);

        if (distance < 0.1f)
        {
            currentPhase = Phase.Landing;
        }
        else
        {
            RotateTowards(flatDest);
            MoveTowards(flatDest, speed);
        }
    }

    void HandleLanding()
    {
        Vector3 landPos = new Vector3(transform.position.x, destination.position.y + 5f, transform.position.z);
        float verticalDist = Mathf.Abs(transform.position.y - landPos.y);

        if (verticalDist < 0.1f)
        {
            currentPhase = Phase.Dropping;
        }
        else
        {
            MoveTowards(landPos, verticalSpeed);
        }
    }

    void HandleDropping()
    {
        if (!doorOpened && !isOpeningDoor)
        {
            isOpeningDoor = true;
            animator.SetBool("OpenDoor", true);
            Invoke(nameof(OnDoorOpened), 1.5f); // adjust to match open animation
            return;
        }

        if (doorOpened && !isClosingDoor)
        {
            if (numberOfSpawns > 0)
            {
                if (dropCountdown <= 0)
                {
                    int enemyToSpawn = Random.Range(0, aliens.Length);
                    Instantiate(aliens[enemyToSpawn], spawnPoint.position, Quaternion.identity);
                    numberOfSpawns--;
                    dropCountdown = dropTimer;
                }
                else
                {
                    dropCountdown -= Time.deltaTime;
                }
            }
            else
            {
                isClosingDoor = true;
                animator.SetBool("OpenDoor", false); // start closing
                Invoke(nameof(OnDoorClosed), 1.5f); // adjust to match close animation
            }
        }
    }

    void HandleTakeoff()
    {
        Vector3 liftOff = transform.position + new Vector3(0, 5f, 0);
        float verticalDistance = 5f;

        if (Vector3.Distance(transform.position, liftOff) < 0.1f)
        {
            currentPhase = Phase.Leaving;
        }
        else
        {
            MoveTowards(liftOff, verticalSpeed);
        }
    }

    void HandleLeaving()
    {
        Vector3 flatOrigin = new Vector3(origin.position.x, transform.position.y, origin.position.z);
        RotateTowards(flatOrigin);
        float distance = Vector3.Distance(transform.position, flatOrigin);
        if (distance < 0.1f)
        {
            Destroy(gameObject);
        }
        else
        {
            MoveTowards(flatOrigin, speed);
        }
    }

    void MoveTowards(Vector3 targetPos, float moveSpeed)
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);
    }

    void RotateTowards(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position);
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void OnDoorOpened()
    {
        doorOpened = true;
        isOpeningDoor = false;
    }

    void OnDoorClosed()
    {
        doorClosed = true;
        currentPhase = Phase.Takeoff;
    }

    public void OriginPoint(Transform _origin)
    {
        origin = _origin;
    }
}
