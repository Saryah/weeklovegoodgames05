using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapons : MonoBehaviour
{
    public int ammunition;
    public int maxAmmunition;
    public float fireRate;
    [SerializeField] float fireTimer = 0;
    public float reloadTime;
    public float reloadTimer = 2f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float range;
    [SerializeField] bool canFire;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reloadTime = reloadTimer;
        GameManager.instance.UpdateAmmo();
        fireTimer = fireRate;
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.currentAmmo <= 0 && ammunition <= 0)
            return;
        if (fireTimer <= 0)
        {
            canFire = true;
        }

        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
            canFire = false;
        }
        if (GameManager.instance.inMenu || GameManager.instance.buildMode)
            return;
        if (Input.GetMouseButton(0) && canFire)
        {
            if (ammunition > 0)
            {
                Shoot();
            }
        }

        if (ammunition <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            Vector3 hitPos = hit.point;
            
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Projectile>().targetPosition = hitPos;

            ammunition--;
            GameManager.instance.UpdateAmmo();
            fireTimer = fireRate;
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);

        int neededAmmo = maxAmmunition - ammunition;

        if (Player.instance.currentAmmo >= neededAmmo)
        {
            ammunition = maxAmmunition;
            Player.instance.currentAmmo -= neededAmmo;
        }
        else
        {
            ammunition += Player.instance.currentAmmo;
            Player.instance.currentAmmo = 0;
        }

        GameManager.instance.UpdateAmmo();
    }
}
