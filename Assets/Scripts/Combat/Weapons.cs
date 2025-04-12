using UnityEngine;

public class Weapons : MonoBehaviour
{
    public int ammunition;
    public int maxAmmunition;
    public float reloadTime;
    public float reloadTimer = 2f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reloadTime = reloadTimer;
        GameManager.instance.UpdateAmmo(ammunition);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.buildMode)
        {
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
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        ammunition--;
        GameManager.instance.UpdateAmmo(ammunition);
    }
}
