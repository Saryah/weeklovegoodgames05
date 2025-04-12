
using UnityEngine;

public class Terret : MonoBehaviour
{
    public Transform target;
    public float range;
    public Transform partToRotate;
    public float rotationSpeed = 10f;
    [SerializeField] float cooldown;
    public float cooldownTimer = 5f;
    private int currentBarrel = 0;
    public Transform[] barrels;
    public GameObject bulletPrefab;

    void Start()
    {
        cooldown = cooldownTimer;
        InvokeRepeating("UpdateTarget", 0, .5f);
    }

    void Update()
    {
        if (target == null)
            return;
        
        //Target Lockon
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation, 
            Time.deltaTime * rotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < range)
        {
            if (cooldown <= 0)
            {
                Shoot();
                cooldown = cooldownTimer;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }

    void UpdateTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        
        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        
        foreach (var _target in targets)
        {
            float distance = Vector3.Distance(_target.transform.position, transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = _target;
            }

            if (closestEnemy != null && distance < range)
            {
                target = closestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }
    }

    void Shoot()
    {
        bulletPrefab.GetComponent<Projectile>().targetPosition = target.transform.position;
        Debug.Log(bulletPrefab.GetComponent<Projectile>().targetPosition);
        Instantiate(bulletPrefab, barrels[currentBarrel].position, bulletPrefab.transform.rotation);
        Debug.Log("Bullet Instanitate at Barrel " + barrels[currentBarrel].name);
        if (currentBarrel == barrels.Length - 1)
        {
            currentBarrel = 0;
        }
        else
        {
            currentBarrel++;
        }
        Debug.Log(currentBarrel);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
