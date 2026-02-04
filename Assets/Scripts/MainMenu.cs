using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ManagementScene()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void BuildScene()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void QuitGame()
    {
        Application.Quit();
        EditorApplication.ExitPlaymode();
    }
}