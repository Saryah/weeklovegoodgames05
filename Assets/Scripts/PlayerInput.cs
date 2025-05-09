using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    public KeyCode switch1 = KeyCode.UpArrow;
    public KeyCode switch2 = KeyCode.DownArrow;
    public KeyCode switch3 = KeyCode.LeftArrow;
    public KeyCode switch4 = KeyCode.RightArrow;
    public KeyCode playerSwap_N = KeyCode.E;
    public KeyCode playerSwap_P = KeyCode.Q;
    public KeyCode characterMenu = KeyCode.I;

    void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }
}
