using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public bool inMenu = false;
    [Space(10)] 
    [Header("Menu")] 
    public GameObject buildMenu;

    [Space(10)] 
    [Header("HUD")] 
    public Image lookAtImg;
    public Image canPlaceImg;
    public Sprite canPlace;
    public Sprite cantPlace;


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
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildMenu.SetActive(true);
            player.GetComponent<FirstPersonController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            inMenu = true;
        }
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
}
