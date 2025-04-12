using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int health;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            int tmpHealth = Player.instance.playerHealth + health;
            if(tmpHealth >+ Player.instance.playerHealth)
                Player.instance.playerHealth = Player.instance.playerMaxHealth;
            else
            {
                Player.instance.playerHealth += health;
            }
            GameManager.instance.UpdateHealth(Player.instance.playerHealth);
            Destroy(gameObject); 
        }
    }
}
