using UnityEngine;

public class Building : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Material[] mats = Resources.LoadAll<Material>("Materials/Buildings");
        int rand = Random.Range(0, mats.Length);
        GetComponent<Renderer>().material = mats[rand];
    }
}
