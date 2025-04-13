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
    public Material playerMaterial;
    public Renderer[] renderers;

    void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }

    void Start()
    {
        playerMaterial = LevelSettings.instance.playerMaterial;
        foreach(Renderer r in renderers)
            r.material = playerMaterial;
        playerHealth = playerMaxHealth;
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        GameManager.instance.UpdateHealth();
    }

}
