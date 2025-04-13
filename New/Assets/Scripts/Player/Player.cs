using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public int playerMoney;
    public int playerHealth;
    public int playerMaxHealth;
    public int currentAmmo = 24;
    public int healthPacks;
    public GameObject weaponHolder;
    public AudioSource audio;

    void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }

    void Start()
    {
        playerHealth = playerMaxHealth;
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        GameManager.instance.UpdateHealth();
    }

}
