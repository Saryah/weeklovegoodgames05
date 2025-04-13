using System.Collections.Generic;
using UnityEngine;

public class NpcAI : MonoBehaviour
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
    [SerializeField] List<GameObject> _targets = new List<GameObject>();
    [SerializeField] private bool isAttacking, notMoving;
    [SerializeField] Transform partToRotate;

    [SerializeField] Rigidbody _rb;

    [Space(10)]
    [Header("Stats")]
    public int artilaryStat;
    public int medicStat;
    public int mechanicStat;

    [Header("NPC Type")]
    public NPCType npcType;

    public int cost;
    public Sprite icon;

    void Start()
    {
        if (npcType == NPCType.Artilary)
        {
            artilaryStat = Random.Range(1, 20);
            medicStat = Random.Range(1, 10);
            mechanicStat = Random.Range(1, 10);
        }
        if (npcType == NPCType.Medic)
        {
            artilaryStat = Random.Range(1, 10);
            medicStat = Random.Range(1, 20);
            mechanicStat = Random.Range(1, 10);
        }
        if (npcType == NPCType.Mechanic)
        {
            artilaryStat = Random.Range(1, 10);
            medicStat = Random.Range(1, 10);
            mechanicStat = Random.Range(1, 20);
        }
        health = GetComponent<HealthComponent>().maxHealth;
        if (npcType == NPCType.Artilary)
        {
            range = artilaryStat;
        }
        GameManager.instance.enemiesOnMap.Add(gameObject);
        attackCooldown = cooldownTimer;
        InvokeRepeating("FindTarget", 0f, 0.5f);
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = false;
    }

    void Update()
    {
        if (GameManager.instance.gamePaused || GameManager.instance.gameOver)
            return;

        switch (npcType)
        {
            case NPCType.Artilary:
                HandleArtilary();
                break;
            case NPCType.Medic:
                HandleMedic();
                break;
            case NPCType.Mechanic:
                HandleMechanic();
                break;
        }
        
        Rotate();
    }

    void FindTarget()
    {
        _targets.Clear();

        switch (npcType)
        {
            case NPCType.Artilary:
                _targets.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
                break;

            case NPCType.Medic:
                _targets.AddRange(GameObject.FindGameObjectsWithTag("NPC"));
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null) _targets.Add(player);
                break;

            case NPCType.Mechanic:
                _targets.AddRange(GameObject.FindGameObjectsWithTag("Obstical"));
                break;
        }

        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;

        foreach (var target in _targets)
        {
            if (npcType != NPCType.Artilary)
            {
                HealthComponent hp = target.GetComponent<HealthComponent>();
                if (hp == null || hp.currentHealth >= hp.maxHealth) continue;
            }

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

    void HandleArtilary()
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
    }

    void HandleMedic()
    {
        if (_target == null) return;

        float distance = Vector3.Distance(transform.position, _target.position);
        if (distance < range)
        {
            HealthComponent hp = _target.GetComponent<HealthComponent>();
            if (hp != null && hp.currentHealth < hp.maxHealth)
            {
                hp.Heal(medicStat);
            }
        }
        else
        {
            Move();
        }
    }

    void HandleMechanic()
    {
        if (_target == null) return;

        float distance = Vector3.Distance(transform.position, _target.position);
        if (distance < range)
        {
            HealthComponent hp = _target.GetComponent<HealthComponent>();
            if (hp != null && hp.currentHealth < hp.maxHealth)
            {
                hp.Heal(mechanicStat);
            }
        }
        else
        {
            Move();
        }
    }

    void Attacking()
    {
        Projectile projectile = bulletPrefab.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.targetPosition = _target.position;
            projectile.projectileTag = "Enemy";
        }
        else
        {
            ExplosiveProjectile explosive = bulletPrefab.GetComponent<ExplosiveProjectile>();
            if (explosive != null)
                explosive.targetPosition = _target.position;
        }

        Instantiate(bulletPrefab, firePoint.position, bulletPrefab.transform.rotation);
        isAttacking = true;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        GetComponent<HealthComponent>().TakeDamage(damage);
        if (health <= 0)
        {
            GameManager.instance.enemiesOnMap.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}

public enum NPCType
{
    None,
    Artilary,
    Medic,
    Mechanic
}
