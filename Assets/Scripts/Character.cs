using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public string charName;
    public Sprite icon;
    public Texture skin;
    public int startPower;
    public int startHealth;
    public int startVitality;
    public int startBody;
    public int startFocus;
    public int startAtkRange;

}
