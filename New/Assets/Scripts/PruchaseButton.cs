using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PruchaseButton : MonoBehaviour
{
    public GameObject objToPurchase;
    Button purchaseButton;
    [SerializeField] TMP_Text objNameTxt, objCostTxt;
    [SerializeField] Image objIcon;

    void Start()
    {
        purchaseButton = GetComponent<Button>();
        objNameTxt.text = objToPurchase.name;
        objCostTxt.text = objToPurchase.GetComponent<Object>().cost.ToString();
        objIcon.sprite = objToPurchase.GetComponent<Object>().icon;
    }
    void Update()
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

    public void Purchase()
    {
        GridSystem.instance.SetObjectToPlace(objToPurchase);
        GameManager.instance.buildMenu.SetActive(false);
        GameManager.instance.inMenu = false;
        GameManager.instance.LockCursor();
    }
}
