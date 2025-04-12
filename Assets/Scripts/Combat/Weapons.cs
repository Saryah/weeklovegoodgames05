using UnityEngine;
using UnityEngine.UIElements;

public class Weapons : MonoBehaviour
{
    public int ammunition;
    public int maxAmmunition;
    public float reloadTime;
    public float reloadTimer = 2f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float range;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reloadTime = reloadTimer;
        GameManager.instance.UpdateAmmo(ammunition);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.inMenu || GameManager.instance.buildMode)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            if (ammunition > 0)
            {
                Shoot();
            }
        }

        if (ammunition <= 0)
        {
            if (reloadTime <= 0)
            {
                ammunition = maxAmmunition;
                reloadTime = reloadTimer;
                GameManager.instance.UpdateAmmo(ammunition); 
            }
            reloadTime -= Time.deltaTime;
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
            GameManager.instance.UpdateAmmo(ammunition);
        }
    }
}
