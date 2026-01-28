using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rooms : MonoBehaviour
{
    public static Rooms Instance;
    public int roomCost = 500;
    public Button newRoomButton;
    public GameObject roomPrefab;

    private void Start()
    {
        Instance = this;
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("Cash", 750) >= roomCost)
        {
            newRoomButton.interactable = true;
        }
        else
        {
            newRoomButton.interactable = false;
        }
    }

    public void BuildNewRoom()
    {
        GameManager.Instance.cash -= roomCost;
        PlayerPrefs.SetInt("Cash", GameManager.Instance.cash);

        roomPrefab.SetActive(false);
        Invoke("BuildScene", 1f);
    }

    public void BuildScene()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
