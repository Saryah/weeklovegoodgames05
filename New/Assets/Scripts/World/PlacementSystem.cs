using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    public static PlacementSystem instance;
    public bool inBuildMode = false;
    public GameObject buildMenu;
    [Space(10)] 
    [Header("Purchase")] 
    public GameObject objectToPlace;
    private GameObject ghostObject;
    private bool canPlace = false;
    
    void Awake()
    {
        if(instance!=null)
            Destroy(instance);
        instance = this;
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buildMenu.SetActive(inBuildMode);
    }

    // Update is called once per frame
    void Update()
    {
        buildMenu.SetActive(inBuildMode);
        if (Input.GetKeyDown(KeyCode.B))
        {
            inBuildMode = !inBuildMode;
            if (inBuildMode)
            {
                GameManager.instance.UnlockCursor();
            }
            else
            {
                GameManager.instance.LockCursor();
            }
            
        }
    }

    public void PurchaseObject(GameObject obj)
    {
        
    }
}
