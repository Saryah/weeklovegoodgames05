using UnityEngine;

[CreateAssetMenu(fileName = "New Placeable Object", menuName = "Scriptable Objects/Placeable Objects")]
public class PlaceableObjects : ScriptableObject
{
    public string ObjectNames;
    public string tag;
    public GameObject prefab;
    public int cost;
    public Sprite sprite;
}
