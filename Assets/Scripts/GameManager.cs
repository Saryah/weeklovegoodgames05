using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] characters;
    public GameObject activeCharacter;
    
    [Space(10)]
    [Header("UI Elements")]
    [Space(10)]
    public Slider healthBar;
    public Text healthText;
    public Slider powerBar;
    public Text powerText;
    public Slider expierienceBar;
    public Text expierienceText;

    public GameObject characterMenu;
    public bool isInMenu = false;

    void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwapCharacter(0);
        SetBars();
        LockUnlockCursor(isInMenu);
        characterMenu.SetActive(isInMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PlayerInput.instance.switch1))
        {
            SwapCharacter(0);
        }

        if (Input.GetKeyDown(PlayerInput.instance.switch2))
        {
            SwapCharacter(1);
        }

        if (Input.GetKeyDown(PlayerInput.instance.switch3))
        {
            SwapCharacter(2);
        }

        if (Input.GetKeyDown(PlayerInput.instance.switch4))
        {
            SwapCharacter(3);
        }

        if (Input.GetKeyDown(PlayerInput.instance.characterMenu))
        {
            isInMenu = !isInMenu;
            LockUnlockCursor(isInMenu);
            characterMenu.SetActive(isInMenu);
        }
    }

    void SwapCharacter(int index)
    {
        Debug.Log(characters[index].name + " swapped");
        foreach (GameObject character in characters)
        {
            CharacterDisplay characterDisplay = character.GetComponent<CharacterDisplay>();
            if (index == characterDisplay.slot)
            {
                characterDisplay.CharacterSwap(true);
                activeCharacter = character;
                SetBars();
            }
            else
            {
                characterDisplay.CharacterSwap(false);
            }
        }
    }

    public void LockUnlockCursor(bool statuse)
    {
        isInMenu = statuse;
        Cursor.visible = isInMenu;
        if (isInMenu)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void SetBars()
    {
        CharacterDisplay characterDisplay = activeCharacter.GetComponent<CharacterDisplay>();
        healthBar.maxValue = characterDisplay.maxHealth;
        healthBar.value = characterDisplay.currentHealth;
        healthText.text = characterDisplay.currentHealth + "/" + characterDisplay.maxHealth;
        powerBar.maxValue = characterDisplay.maxPower;
        powerBar.value = characterDisplay.currentPower;
        powerText.text = characterDisplay.currentPower + "/" + characterDisplay.maxPower;
        expierienceBar.maxValue = characterDisplay.maxExpirience;
        expierienceBar.value = characterDisplay.currentExpirience;
        expierienceText.text = characterDisplay.currentExpirience + "/" + characterDisplay.maxExpirience;
    }
}
