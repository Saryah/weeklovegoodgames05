using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLevelUp : MonoBehaviour
{
    public static CharacterLevelUp Instance;
    private int index;
    [Header("UI Elements")] 
    [Space(10)] 
    public Image profDisplay;
    public Text charNameTxt;
    public Text levelTxt;
    public Text expTxt;
    public Text healthTxt;
    public Text powerTxt;
    public Text availablePointsTxt;
    
    public Slider healthSlider;
    public Slider powerSlider;
    public Slider expSlider;

    public Text bodyTxt;
    public Text focusTxt;
    public Text rangeTxt;
    public Text vitalityTxt;

    public List<GameObject> party;
    public GameObject displayMember;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject member in GameManager.instance.characters)
        {
            party.Add(member);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PlayerInput.instance.playerSwap_N))
        {
            index++;
            
            if(index >= party.Count)
                index = 0;
            Debug.Log("Is on index " + index);
        }

        if (Input.GetKeyDown(PlayerInput.instance.playerSwap_P))
        {
            index--;
            
            if(index < 0)
                index = party.Count - 1;
            Debug.Log("Is on index " + index);
        }
        displayMember = party[index];
        CharacterDisplay display = displayMember.GetComponent<CharacterDisplay>();
        profDisplay.sprite = display.character.icon;
        charNameTxt.text ="Name: " + display.character.charName;
        levelTxt.text = "Level: " + display.lvl;
        expTxt.text = display.currentExpirience + "/" + display.maxExpirience;
        healthTxt.text = display.currentHealth + "/" + display.maxHealth;
        powerTxt.text = display.currentPower + "/" + display.maxPower;
        availablePointsTxt.text = display.pointsToUse.ToString();
        bodyTxt.text = display.body.ToString();
        focusTxt.text = display.focus.ToString();
        rangeTxt.text = display.atkRange.ToString();
        vitalityTxt.text = display.vitality.ToString();
        healthSlider.maxValue = display.maxHealth;
        powerSlider.maxValue = display.maxPower;
        expSlider.maxValue = display.maxExpirience;
        healthSlider.value = display.currentHealth;
        powerSlider.value = display.currentPower;
        expSlider.value = display.currentExpirience;
    }
}
