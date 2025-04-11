using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    [SerializeField] Transform _target;
    [SerializeField] List<GameObject> _targets;
    [SerializeField] private bool isAttacking, notMoving;
    [SerializeField] Transform partToRotate;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackCooldown = cooldownTimer;
        InvokeRepeating("FindTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
            return;
        if (Vector3.Distance(transform.position, _target.transform.position) < range)
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
        GameObject[] targets = GameObject.FindGameObjectsWithTag("NPC");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _targets.AddRange(targets);
        _targets.Add(player);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;
        
        foreach (var target in _targets)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget < shortestDistance)
            {
                shortestDistance = distanceToTarget;
                nearestTarget = target;
            }
        }
        if(nearestTarget != null)
            _target = nearestTarget.transform;
        else
        {
            _target = null;
        }
    }

    void Move()
    {
        Vector3 dir = _target.transform.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }

    void Rotate()
    {
        Vector3 dir = _target.transform.position - transform.position;
        
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime *rotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Attacking()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
