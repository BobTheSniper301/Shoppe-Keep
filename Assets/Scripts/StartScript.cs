using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("DefaultArea");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
