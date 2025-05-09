using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    [Space(10)]
    [Header("Character Info")]
    [Space(10)]
    public Character character;
    public Renderer[] renderers;
    public Material mat;
    public bool isActive;
    public int slot;
    public float currentHealth;
    public float maxHealth;
    public float currentPower;
    public float maxPower;
    public float currentExpirience;
    public float maxExpirience;
    public int vitality;
    public int body;
    public int focus;
    public int atkRange;
    public int lvl;
    public int pointsToUse;
    
    //Camera Settings
    [Space(10)]
    [Header("Camera")]
    [Space(10)]
    private Camera cam;
    
    [Space(10)]
    [Header("UI Display")]
    [Space(10)]
    public Image image;
    public Sprite icon;
    public Image selectedIcon;
    public Sprite selectedSprite;
    public Sprite notSelectedSprite;

    [Space(10)] 
    [Header("Character Movement")] 
    [Space(10)]
    public float speed = 5f;

    void Awake()
    {
        mat = new Material(Resources.Load("Skins/Materials/CharacterSkin") as Material);
        cam = GetComponentInChildren<Camera>();
        icon = character.icon;
        mat.mainTexture = character.skin;
        foreach (Renderer rend in renderers)
        {
            rend.material = mat;
        }
        image.sprite = icon;
        maxHealth = character.startHealth;
        currentHealth = maxHealth;
        maxPower = character.startPower;
        currentPower = maxPower;
        maxExpirience = 300;
        currentExpirience = 0;
        lvl = 1;
        body = character.startBody;
        focus = character.startFocus;
        atkRange = character.startAtkRange;
        vitality = character.startVitality;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            cam.gameObject.SetActive(false);
            selectedIcon.sprite = notSelectedSprite;
        }
        else
        {
            cam.gameObject.SetActive(true);
            selectedIcon.sprite = selectedSprite;
        }
    }

    public void CharacterSwap(bool _isActive)
    {
        isActive = _isActive;
    }

    public void SetHealth(int health)
    {
        currentHealth -= health;
        GameManager.instance.SetBars();
    }

    public void SetPower(int power)
    {
        currentPower -= power;
        GameManager.instance.SetBars();
    }

    public void SetExpirience(int expirience)
    {
        currentExpirience += expirience;
        GameManager.instance.SetBars();
    }
}
