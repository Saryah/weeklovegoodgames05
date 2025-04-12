using System;
using Unity.VisualScripting;
using UnityEngine;

public class PulseCore : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player.instance.playerMoney++;
            GameManager.instance.UpdateMoney();
            Destroy(gameObject); 
        }
    }
}
