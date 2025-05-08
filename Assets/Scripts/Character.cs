using UnityEngine;

public class Character : MonoBehaviour
{
    public Ranger ranger;
    public Renderer[] rends;
    private Material mat;

    void Start()
    {
        
        mat = new Material(Resources.Load<Material>("Skins/Materials/RangerSkin"));
        mat.mainTexture = ranger.suit;
        foreach (var rend in rends)
        {
            rend.material = mat;
        }
    }
}
