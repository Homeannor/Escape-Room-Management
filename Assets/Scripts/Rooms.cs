using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rooms : MonoBehaviour
{
    public static Rooms Instance;
    public GameObject roomPrefab;
    private int roomCooldown;
    private int timeInRoom;
    public int roomCost = 500;
    private bool hasChecked;
    private bool hasSpawnedCustomers;

    [Header("Booleans for Button changes")]
    public bool boughtRoom;
    public bool isReady;
    public bool hasCustomers;
    public bool needsResetting;
    //public bool needsHint;

    [Header("Buttons")]
    public GameObject background;
    public GameObject newRoomButton;
    public GameObject editRoomButton;
    public GameObject resetRoomButton;
    //public GameObject hintButton;

    [Header("Spawn Customers")]
    public int groupNumber;
    public GameObject customerPrefab;
    public GameObject customerSpawnPoint;
    public Transform customerFolder;

    public SavingManager savingManager;
    // public Transform roomItemCube;

    private void Start()
    {
        Instance = this;
        background.SetActive(true);
        newRoomButton.SetActive(true);
        editRoomButton.SetActive(false);
        resetRoomButton.SetActive(false);
        boughtRoom = false;
        isReady = false;
        hasCustomers = false;
        needsResetting = false;

        savingManager.LoadRoom(1);
        savingManager.roomContainer.position -= savingManager.roomContainer.position;
        //savingManager.roomContainer.rotation = Quaternion.Euler(0f, -90f, 0f);

        // roomItemCube.SetParent(transform);
        // roomItemCube.position = new Vector3(0, -0.41f, 0);
        // roomItemCube.rotation = Quaternion.Euler(0f, -90f, 0f);
        // Debug.Log("[START] Moved Loaded Items to correct position");
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
        if (boughtRoom == true)
        {
            if (hasChecked == false)
            {
                background.SetActive(true);
                newRoomButton.SetActive(false);
                editRoomButton.SetActive(true);
                resetRoomButton.SetActive(false);
                isReady = true;
                hasChecked = true;
            }
        }
        if (isReady == true)
        {
            if (hasSpawnedCustomers == false)
            {
                if (GameManager.instance.isClosed == false)
                {
                    StartCoroutine(SpawnCustomers());
                    roomCooldown = Random.Range(5, 10);
                    timeInRoom = Random.Range(20, 30);
                    hasSpawnedCustomers = true;
                }
                else
                {
                    //Debug.Log("Customers cannot spawn at night");
                    return;
                }
            }
        }
        if (hasCustomers == true)
        {
            //customers are in room
            background.SetActive(false);
            newRoomButton.SetActive(false);
            editRoomButton.SetActive(false);
            resetRoomButton.SetActive(false);           
        }
        if (boughtRoom == true && hasCustomers == false && needsResetting == false)
        {
            //room is empty ready for next group so can be edited
            isReady = true;
            background.SetActive(true);
            newRoomButton.SetActive(false);
            editRoomButton.SetActive(true);
            resetRoomButton.SetActive(false);
        }
        if (needsResetting == true)
        {
            //room needs resetting for next group
            background.SetActive(true);
            newRoomButton.SetActive(false);
            editRoomButton.SetActive(false);
            resetRoomButton.SetActive(true);
            //hintButton.SetActive(false);
        }
    }

    public void BuildNewRoom()
    {
        GameManager.instance.cash -= roomCost;
        PlayerPrefs.SetInt("Cash", GameManager.instance.cash);
        boughtRoom = true;
        roomPrefab.SetActive(false);
        //Commented out for testing purposes
        //Invoke("BuildScene", 1f);
    }

    public void BuildScene()
    {
        SceneManager.LoadSceneAsync(2);
    }

    private IEnumerator SpawnCustomers()
    {
        yield return new WaitForSeconds(roomCooldown); //cooldown between romm being ready and customers spawning so its not instant
        groupNumber = Random.Range(2, 5);
        for (int i = 0; i < groupNumber; i++)
        {
            Instantiate(customerPrefab, customerSpawnPoint.transform.position, transform.rotation, customerFolder);
        }
        isReady = false;
        hasCustomers = true;
        Invoke("CustomersLeave", timeInRoom);
    }

    public void CustomersLeave()
    {
        //Debug.Log("Customer Left");
        foreach (Transform customer in customerFolder)
        {
            Destroy(customer.gameObject);
        }
        GameManager.instance.cash += 100;
        hasCustomers = false;
        hasSpawnedCustomers = false;
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
}
