// using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SavingManager savingManager;

    public void ManagementScene()
    {
        SceneManager.LoadSceneAsync(1);
        savingManager.SaveRoom(1);
    }

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void NewGame()
    {
        PlayerPrefs.SetFloat("Days", 1f);
        PlayerPrefs.SetFloat("TimeOfDay", 20f);
        PlayerPrefs.SetInt("Cash", 750);
        PlayerPrefs.SetString("Room1", "False");
        PlayerPrefs.SetString("IsClosed", "True");

        savingManager.SaveRoom(1);
        SceneManager.LoadSceneAsync(1);
    }

    public void MenuThing()
    {
        Invoke("BuildScene", 1f);
    }

    public void QuitGame()
    {
        Application.Quit();
        // EditorApplication.ExitPlaymode();
    }

    public void BuildScene()
    {
        SceneManager.LoadSceneAsync(0);
    }
}