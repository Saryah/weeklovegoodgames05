using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpens : MonoBehaviour
{
    public Scene scene;
    public GameObject interaction;
    private bool _canTransfer = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        interaction.SetActive(_canTransfer);
        if (Input.GetKeyDown(KeyCode.E) && _canTransfer)
        {
            SceneManager.LoadScene("Dungeon1");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        _canTransfer = true;
    }

    void OnTriggerExit(Collider other)
    {
        _canTransfer = false;
    }
}
