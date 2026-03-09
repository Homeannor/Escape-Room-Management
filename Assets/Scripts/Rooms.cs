using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rooms : MonoBehaviour
{
    public static Rooms Instance;
    public int roomCost = 500;
    public GameObject roomPrefab;

    [Header("Booleans for Button changes")]
    public bool boughtRoom;
    public bool isReady;
    public bool hasCustomers;
    public bool needsResetting;
    public bool needsHint;

    [Header("Buttons")]
    public GameObject background;
    public GameObject newRoomButton;
    public GameObject editRoomButton;
    public GameObject resetRoomButton;
    public GameObject hintButton;

    private void Start()
    {
        Instance = this;
        background.SetActive(true);
        newRoomButton.SetActive(true);
        editRoomButton.SetActive(false);
        resetRoomButton.SetActive(false);
        hintButton.SetActive(false);
        boughtRoom = false;
        isReady = false;
        hasCustomers = false;
        needsResetting = false;
        needsHint = false;
}
    void Update()
    {
        if (PlayerPrefs.GetInt("Cash", 750) >= roomCost)
        {
            newRoomButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            newRoomButton.GetComponent<Button>().interactable = false;
        }
        if (isReady == true)
        {
            SpawnCustomers();
        }
        if (boughtRoom == true)
        {
            background.SetActive(true);
            newRoomButton.SetActive(false);
            editRoomButton.SetActive(true);
            resetRoomButton.SetActive(false);
            hintButton.SetActive(false);
            isReady = true;
        }
        if (hasCustomers == true)
        {
            //customers are in room
            background.SetActive(false);
            newRoomButton.SetActive(false);
            editRoomButton.SetActive(false);
            resetRoomButton.SetActive(false);
            hintButton.SetActive(false);
            if (needsHint == true)
            {
                //if the customers need a hint
                hintButton.SetActive(true);
                background.SetActive(true);
            }
        }
        if (boughtRoom == true && hasCustomers == false && needsResetting == false)
        {
            //room is empty ready for next group so can be edited
            background.SetActive(true);
            newRoomButton.SetActive(false);
            editRoomButton.SetActive(true);
            resetRoomButton.SetActive(false);
            hintButton.SetActive(false);
        }
        if (needsResetting == true)
        {
            //room needs resetting for next group
            background.SetActive(true);
            newRoomButton.SetActive(false);
            editRoomButton.SetActive(false);
            resetRoomButton.SetActive(true);
            hintButton.SetActive(false);
        }
    }

    public void BuildNewRoom()
    {
        GameManager.Instance.cash -= roomCost;
        PlayerPrefs.SetInt("Cash", GameManager.Instance.cash);
        boughtRoom = true;
        roomPrefab.SetActive(false);
        //Commented out for testing purposes
        //Invoke("BuildScene", 1f);
    }

    public void BuildScene()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void SpawnCustomers()
    {
        Debug.Log("Customer Spawned");
        isReady = false;
        hasCustomers = true;
        Invoke("CustomersLeave", 5);
    }

    public void CustomersLeave()
    {
        Debug.Log("Customer Left");
        hasCustomers = false;
        needsResetting = true;
    }

    public void EditRoom()
    {
        //Takes you back to build area
        Debug.Log("Edit");
    }
    
    public void ResetRoom()
    {
        Debug.Log("Reset");
        resetRoomButton.SetActive(false);
        background.SetActive(false);
        needsResetting = false;
    }

    public void GiveHint()
    {
        Debug.Log("Gave Hint");
        hintButton.SetActive(false);
        background.SetActive(false);
        needsHint = false;
    }
}
