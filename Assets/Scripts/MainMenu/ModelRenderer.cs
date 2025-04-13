using UnityEngine;

public class ModelRenderer : MonoBehaviour
{
    public Renderer[] renderers;
    public Material mat;
    private float rotationSpeed = 100;

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
        }
    }
    
    public void RenderModel()
    {
        foreach (Renderer r in renderers)
        {
            r.material = mat;
        }
    }
}
