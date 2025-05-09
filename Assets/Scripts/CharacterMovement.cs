using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    public CharacterDisplay display;
    private Rigidbody rb;
    private Transform cameraTransform;

    private float currentYaw;
    public float rotationSpeed = 100f;
    public float moveSpeed = 5f;

    public float maxFollowDistance = 10f;
    public float stopDistance = 0.1f;

    private bool isFollowing = false;

    [Header("Enemy Detection")]
    public float enemyDetectionRadius;
    public LayerMask enemyLayer;
    public Transform currentEnemy;

    void Start()
    {
        display = GetComponent<CharacterDisplay>();
        rb = GetComponent<Rigidbody>();
        cameraTransform = GetComponentInChildren<Camera>()?.transform;
        enemyDetectionRadius = display.atkRange; // int-to-float conversion is implicit
    }

    void Update()
    {
        // Handle player rotation input (even if physics happens in FixedUpdate)
        if (display.isActive)
        {
            currentYaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, currentYaw, 0);
        }
    }

    void FixedUpdate()
    {
        if (display.isActive)
        {
            HandlePlayerMovement();
        }
        else
        {
            HandleAIMovement();
        }
    }

    void HandlePlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(horizontal, 0, vertical).normalized;
        if (inputDir.magnitude >= 0.1f)
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDir = camForward * vertical + camRight * horizontal;

            // Move the player
            Vector3 targetPosition = rb.position + moveDir * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(targetPosition);

            // Rotate smoothly in movement direction
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            Quaternion smoothRot = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(smoothRot);
        }
    }

    void HandleAIMovement()
    {
        Transform target = GameManager.instance.activeCharacter.transform;

        // Find closest enemy
        currentEnemy = FindClosestEnemy();

        if (currentEnemy != null)
        {
            isFollowing = false; // Stop following player if enemy found

            Vector3 dir = (currentEnemy.position - transform.position).normalized;
            rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(Quaternion.LookRotation(dir));
        }
        else
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (!isFollowing && distance > maxFollowDistance)
                isFollowing = true;

            if (isFollowing && distance <= stopDistance)
            {
                isFollowing = false;
                return;
            }

            if (isFollowing)
            {
                Vector3 dir = (target.position - transform.position).normalized;
                rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
                rb.MoveRotation(Quaternion.LookRotation(dir));
            }
        }
    }

    Transform FindClosestEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, enemyDetectionRadius, enemyLayer);
        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closest = enemy.transform;
            }
        }

        return closest;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRadius);
    }
}
