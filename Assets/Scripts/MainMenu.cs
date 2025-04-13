using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject model;
    public void SetDifficulty(string difficulty)
    {
        LevelSettings.instance.difficulty = difficulty;
    }

    public void SelectSkin(Material skin)
    {
        LevelSettings.instance.playerMaterial = skin;
        model.GetComponent<ModelRenderer>().mat = skin;
        model.GetComponent<ModelRenderer>().RenderModel();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    
    void ExitGame()
    {
        Application.Quit();
    }
    
}
