using System;
using UnityEngine;
using Random = System.Random;

public class ItemPickup : MonoBehaviour
{
    public ItemType itemType;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (itemType == ItemType.PulseCore)
            {
                Player.instance.playerMoney++;
                GameManager.instance.UpdateMoney();
            }
            else if (itemType == ItemType.HealthPack)
            {
                Player.instance.healthPacks++;
                GameManager.instance.UpdateHealthPack();
            }
            else if (itemType == ItemType.Ammo)
            {
                int randNum = UnityEngine.Random.Range(1, 11);
                Player.instance.currentAmmo += randNum;
                GameManager.instance.UpdateAmmo();
            }
            Destroy(gameObject);
        }
    }
}

public enum ItemType
{
    PulseCore,
    HealthPack,
    Ammo
}
