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
        if (GameManager.Instance.cash >= 500)
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
        roomPrefab.SetActive(false);
        Invoke("BuildScene", 3f);
    }

    public void BuildScene()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
