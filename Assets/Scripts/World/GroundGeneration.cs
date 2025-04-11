using Unity.AI.Navigation;
using UnityEngine;

public class GroundGeneration : MonoBehaviour
{
    public static GroundGeneration instance;
    public int minL, minW, maxL, maxW;
    [SerializeField] GameObject ground, worldBorder;
    [SerializeField] Transform groundHolder;
    

    void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        for (int l = minL; l < maxL; l++)
        {
            for (int w = minW; w < maxW; w++)
            {
                if (l > minL && l < maxL && w > minW && w < maxW)
                {
                    Vector3 spawnPos = new Vector3(l, 0, w);
                    ground.gameObject.tag = "Ground";
                    Instantiate(ground, spawnPos, Quaternion.identity, groundHolder);
                }
            }
        }
        // Border tiles (around edges)
        for (int l = minL - 1; l <= maxL; l++)
        {
            for (int w = minW - 1; w <= maxW; w++)
            {
                // Only edges (skip inner area)
                if (l == minL || l == maxL || w == minW || w == maxW)
                {
                    Vector3 spawnPos = new Vector3(l, 0, w);
                    Instantiate(worldBorder, spawnPos, Quaternion.identity, groundHolder);
                }
            }
        }
        PlayerBase.instance.SetBase();
    }
}
