using UnityEngine;

public class Pictures : MonoBehaviour
{
    public Ranger[] rangers;
    public Material mat;
    public Renderer[] rends;
    private int index;

    void Start()
    {
        mat = new Material(Resources.Load<Material>("Skins/Materials/RangerSkin"));
        rangers = Resources.LoadAll<Ranger>("Rangers");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (index <= 0)
            {
                index = rangers.Length - 1;
            }
            else
            {
                index--;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (index >= rangers.Length - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ScreenCapture.CaptureScreenshot("Screenshot_" + rangers[index].suit.name + ".png");
        }
        mat.mainTexture = rangers[index].suit;
        foreach (Renderer rend in rends)
        {
            rend.material = mat;
        }
    }
}
