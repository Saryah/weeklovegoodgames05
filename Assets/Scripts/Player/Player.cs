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
    public int kills = 0;
    public int lastMilestone;
    public GameObject[] npcs;

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

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        GameManager.instance.UpdateHealth();
    }

    void CheckMilestone()
    {
        if (kills % 25 == 0 && kills != 0 && kills != lastMilestone)
        {
            lastMilestone = kills;
            SpawnNPC();
        }
    }

    void SpawnNPC()
    {
        int randNpc = Random.Range(0, npcs.Length);
        Instantiate(npcs[randNpc], new Vector3 (0,1,0), Quaternion.identity);
    }

    public void AddKills()
    {
        kills++;
        CheckMilestone();
    }

}
