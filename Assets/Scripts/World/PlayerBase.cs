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
        //GameManager.instance.buildMenu.SetActive(false);
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
