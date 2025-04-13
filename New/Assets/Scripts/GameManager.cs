using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public bool inMenu = false;
    public bool buildMode = false;
    public bool gameOver = false;
    public int level;
    public List<GameObject> enemiesOnMap = new List<GameObject>();
    [Space(10)] 
    [Header("Menu")] 
    public GameObject buildMenu;
    public TMP_Text waveTimerText;
    public GameObject gameOverMenu;
    [Space(10)] 
    [Header("HUD")] 
    public TMP_Text healthTxt;
    public TMP_Text ammoTxt;
    public TMP_Text moneyTxt;
    public TMP_Text healthPackTxt;
    public TMP_Text waveNumberTxt;
    [Space(10)]
    [Header("Audio")]
    public AudioClip healClip;


void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        instance = this;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        buildMenu.SetActive(false);
        UpdateHealth();
        UpdateMoney();
        UpdateAmmo();
        UpdateHealthPack();
        gameOverMenu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (gameOver)
            return;
        if (!WaveSpawner.instance.isWaveStarted)
        {
            waveTimerText.gameObject.SetActive(true);
            
            float countdown = WaveSpawner.instance.waveCountdown;
            int minutes = Mathf.FloorToInt(countdown / 60f);
            int seconds = Mathf.FloorToInt(countdown % 60f);
            int milliseconds = Mathf.FloorToInt((countdown * 1000f) % 1000f);

            waveTimerText.text = $"Aliens Incoming {minutes:00}:{seconds:00}:{milliseconds:000}";
        }
        else
        {
            waveTimerText.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (inMenu)
            {
                buildMenu.SetActive(false);
                inMenu = false;
                LockCursor();
            }
            else
            {
                buildMenu.SetActive(true);
                inMenu = true;
                UnlockCursor();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            buildMode = false;
            buildMenu.SetActive(false);
            inMenu = false;
            LockCursor();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Player.instance.healthPacks > 0)
            {
                int testNum = Player.instance.playerHealth + 15;
                if(testNum >= Player.instance.playerMaxHealth)
                    Player.instance.playerHealth = Player.instance.playerMaxHealth;
                else
                    Player.instance.playerHealth += 15;
                Player.instance.healthPacks--;
                UpdateHealth();
                UpdateHealthPack();
                Player.instance.audio.PlayOneShot(healClip);
            }
        }

        if (Player.instance.playerHealth <= 0)
        {
            gameOver = true;
            gameOverMenu.SetActive(true);
            inMenu = true;
            UnlockCursor();
            player.GetComponent<FirstPersonController>().enabled = false;
        }
        
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<FirstPersonController>().enabled = true;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void UpdateHealth()
    {
        healthTxt.text = Player.instance.playerHealth.ToString();
    }

    public void UpdateHealthPack()
    {
        healthPackTxt.text = Player.instance.healthPacks.ToString();
    }

    public void UpdateAmmo()
    {
        int ammo = Player.instance.weaponHolder.GetComponentInChildren<Weapons>().ammunition;
        ammoTxt.text = ammo + "/" + Player.instance.currentAmmo;
    }
    public void UpdateMoney()
    {
        moneyTxt.text = Player.instance.playerMoney.ToString();
    }

    public void RestartLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void UpdateWave()
    {
        waveNumberTxt.text = "Wave: " + level;
    }
}
