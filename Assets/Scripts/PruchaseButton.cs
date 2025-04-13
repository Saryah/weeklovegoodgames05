using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PurchaseType
{
    Obstical,
    Items
}
public class PruchaseButton : MonoBehaviour
{
    public PurchaseType purchaseType;
    public GameObject objToPurchase;
    Button purchaseButton;
    [SerializeField] TMP_Text objNameTxt, objCostTxt;
    [SerializeField] Image objIcon;
    [SerializeField] Sprite icon;
    [SerializeField] int cost;
    [SerializeField] string objName;
    [SerializeField] int amountToPurchase;
    

    void Start()
    {
        if (purchaseType == PurchaseType.Obstical)
        {
            purchaseButton = GetComponent<Button>();
            objNameTxt.text = objToPurchase.name;
            objCostTxt.text = objToPurchase.GetComponent<Object>().cost.ToString();
            objIcon.sprite = objToPurchase.GetComponent<Object>().icon;
        }
        else
        {
            purchaseButton = GetComponent<Button>();
            objNameTxt.text =amountToPurchase + " " + objName;
            objCostTxt.text = cost.ToString();
            objIcon.sprite = icon;
        }
        
    }
    void Update()
    {
        if (purchaseType == PurchaseType.Obstical)
        {
            if (Player.instance.playerMoney < objToPurchase.GetComponent<Object>().cost)
            {
                purchaseButton.interactable = false;
                objCostTxt.color = Color.red;
            }
            else
            {
                purchaseButton.interactable = true;
                objCostTxt.color = Color.white;
            }
        }

        if (purchaseType == PurchaseType.Items)
        {
            if (Player.instance.playerMoney < cost)
            {
                purchaseButton.interactable = false;
                objCostTxt.color = Color.red;
            }
            else
            {
                purchaseButton.interactable = true;
                objCostTxt.color = Color.white;
            }
        }
        
    }

    public void Purchase()
    {
        GridSystem.instance.SetObjectToPlace(objToPurchase);
        GameManager.instance.buildMenu.SetActive(false);
        GameManager.instance.inMenu = false;
        GameManager.instance.LockCursor();
    }

    public void PurchaseAmmo()
    {
        Player.instance.playerMoney -= cost;
        Player.instance.currentAmmo += amountToPurchase;
        GameManager.instance.buildMenu.SetActive(false);
        GameManager.instance.inMenu = false;
        GameManager.instance.LockCursor();
        GameManager.instance.UpdateAmmo();
        GameManager.instance.UpdateMoney();
    }
    public void PurchaseHealthPack()
    {
        Player.instance.playerMoney -= cost;
        Player.instance.healthPacks += amountToPurchase;
        GameManager.instance.buildMenu.SetActive(false);
        GameManager.instance.inMenu = false;
        GameManager.instance.LockCursor();
        GameManager.instance.UpdateHealthPack();
        GameManager.instance.UpdateMoney();
    }
}
