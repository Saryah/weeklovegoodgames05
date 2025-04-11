using UnityEngine;

public class PlayerBase : MonoBehaviour
{

    public static PlayerBase instance;
    public int cost;
    public int baseIncrease;
    public int baseSizeL, baseSizeW;
    public Material baseMat;
    public bool isPurchasingFloor;

    void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            GameManager.instance.lookAtImg.sprite = hit.collider.gameObject.GetComponent<Object>().objSprite;
            if (isPurchasingFloor)
             {
                if (hit.collider.gameObject.tag == "Ground")
                {
                    GameManager.instance.canPlaceImg.sprite = GameManager.instance.canPlace;
                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.collider.gameObject.GetComponent<Renderer>().material = baseMat;
                        hit.collider.gameObject.GetComponent<Object>().objSprite = Resources.Load<Sprite>("UI/ButtonUI/Floor");
                        isPurchasingFloor = false;
                        IncreaseCost();
                    }
                }
                else
                {
                    GameManager.instance.canPlaceImg.sprite = GameManager.instance.cantPlace;
                }
            }
            else
            {
                GameManager.instance.lookAtImg.sprite = null;
            }
        }
    }
    public void SetBase()
    {
        GameObject[] ground = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject groundObj in ground)
        {
            for (int l = -baseSizeL; l <= baseSizeL; l++)
            {
                for (int w = -baseSizeW; w <= baseSizeW; w++)
                {
                    if (groundObj.transform.position.x == l && groundObj.transform.position.z == w)
                    {
                        Renderer[] renderers = groundObj.GetComponentsInChildren<Renderer>();
                        groundObj.GetComponent<Object>().objSprite = Resources.Load<Sprite>("UI/ButtonUI/Floor");
                        foreach (var renderer in renderers)
                        {
                            renderer.material = baseMat;
                        }

                        groundObj.tag = "Floor";
                    }
                }
            }
        }
        
    }

    public void PurchaseFloor()
    {
        isPurchasingFloor = true;
        GameManager.instance.buildMenu.SetActive(false);
        GameManager.instance.inMenu = false;
        GameManager.instance.LockCursor();
        GameManager.instance.player.GetComponent<FirstPersonController>().enabled = true;
    }

    void IncreaseCost()
    {
        Player.instance.playerMoney -= cost;
        cost *= cost;
    }
}
