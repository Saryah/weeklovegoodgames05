using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public int playerMoney;

    void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }
    
}
