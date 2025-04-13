using Unity.AI.Navigation;
using UnityEngine;

public class GroundGeneration : MonoBehaviour
{
    public static GroundGeneration instance;
    public int minL, minW, maxL, maxW, spawnMinL, spawnMaxL, spawnMinW, spawnMaxW;
    [SerializeField] GameObject ground, worldBorder, spawnPoint;
    public Transform groundHolder, spawnPointHolder;
    

    void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
        SpawnWorld();
        SpawnSpawnPoints();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void SpawnWorld()
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
    }

    void SpawnSpawnPoints()
    {
        // Border tiles (around edges)
        for (int l = spawnMinL - 1; l <= spawnMinL; l++)
        {
            for (int w = spawnMinW - 1; w <= spawnMaxW; w++)
            {
                // Only edges (skip inner area)
                if (l == spawnMinL || l == spawnMinL || w == spawnMaxW || w == spawnMinW)
                {
                    Vector3 spawnPos = new Vector3(l, 20, w);
                    Instantiate(spawnPoint, spawnPos, Quaternion.identity, spawnPointHolder);
                }
            }
        }
    }
}
