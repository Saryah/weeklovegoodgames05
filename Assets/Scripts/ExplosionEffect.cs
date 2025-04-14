using System.Collections;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public Animation anim;
    public float duration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyExplosion());
    }

    IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
