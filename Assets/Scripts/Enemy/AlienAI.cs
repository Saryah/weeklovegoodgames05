using UnityEngine;
using System.Collections.Generic;

public class AlienAI : MonoBehaviour
{
    public int health;
    public int attack;
    public float range;
    public float rotationSpeed;
    public float speed;
    public float attackCooldown;
    public float cooldownTimer;
    public Transform firePoint;
    public GameObject bulletPrefab;

    [SerializeField] private GameObject pulseCorePrefab;
    [SerializeField] Transform _target;
    [SerializeField] List<GameObject> _targets = new List<GameObject>();
    [SerializeField] private bool isAttacking, notMoving;
    [SerializeField] Transform partToRotate;

    [SerializeField] Rigidbody _rb;

    void Start()
    {
        GameManager.instance.enemiesOnMap.Add(gameObject);
        attackCooldown = cooldownTimer;
        InvokeRepeating("FindTarget", 0f, 0.5f);
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = false;
    }

    void Update()
    {
        if (_target == null) return;

        float distance = Vector3.Distance(transform.position, _target.position);
        
        if (distance < range)
        {
            if (attackCooldown <= 0)
            {
                Attacking();
                attackCooldown = cooldownTimer;
            }
            else
            {
                attackCooldown -= Time.deltaTime;
            }
        }
        else
        {
            isAttacking = false;
            Move();
        }

        Rotate();
    }

    void FindTarget()
    {
        _targets.Clear();
        GameObject[] targets = GameObject.FindGameObjectsWithTag("NPC");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _targets.AddRange(targets);
        _targets.Add(player);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;

        foreach (var target in _targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestTarget = target;
            }
        }

        _target = nearestTarget != null ? nearestTarget.transform : null;
    }

    void Move()
    {
        if (notMoving || _target == null) return;

        Vector3 direction = (_target.position - transform.position).normalized;
        Vector3 movePosition = transform.position + direction * speed * Time.deltaTime;
        _rb.MovePosition(movePosition);
    }

    void Rotate()
    {
        if (_target == null) return;

        Vector3 direction = (_target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Quaternion smoothedRotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            _rb.MoveRotation(smoothedRotation);
        }
    }

    void Attacking()
    {
        Instantiate(bulletPrefab, firePoint.position, bulletPrefab.transform.rotation);
        isAttacking = true;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.instance.enemiesOnMap.Remove(gameObject);
            if (GameManager.instance.enemiesOnMap.Count <= 0)
            {
                WaveSpawner.instance.isWaveStarted = false;
                GameManager.instance.level++;
            }
            Vector3 pulseSpawn =new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
            Instantiate(pulseCorePrefab,pulseSpawn,Quaternion.identity);
            Destroy(gameObject,0.1F);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _rb.isKinematic = true;
        }
    }
}
