using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("DefaultArea");
        Time.timeScale = 1;
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
