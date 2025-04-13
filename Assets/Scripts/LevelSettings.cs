using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public static LevelSettings instance;
    public string difficulty;
    public Material playerMaterial;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if(instance !=null)
            Destroy(instance);
        instance = this;
    }
}
