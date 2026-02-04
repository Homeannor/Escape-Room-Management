using UnityEngine;
using UnityEngine.AI;

public class AIInteraction : MonoBehaviour
{
    public GameObject closedChest;
    public GameObject openChest;
    public GameObject closedTomb;
    public GameObject openTomb;
    public GameObject closedDoor;
    public GameObject openDoor;
    public GameObject key; 
    public bool hasKey = false;   

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Customer touched an object");
        
        GetComponent<AIMovement>().StopMoving();
        
        if (other.CompareTag("Chest"))
        {
            //open chest if they have key
            if (hasKey == true)
            {
                closedChest.SetActive(false);
                openChest.SetActive(true);
            }
            GetComponent<AIMovement>().Invoke("StartMoving", 5f);
        }
        if (other.CompareTag("Tomb"))
        {
            //open tomb
            closedTomb.SetActive(false);
            openTomb.SetActive(true);
            GetComponent<AIMovement>().Invoke("StartMoving", 5f);
        }
        if (other.CompareTag("Door"))
        {
            //open door
            closedDoor.SetActive(false);
            openDoor.SetActive(true);
            GetComponent<AIMovement>().Invoke("StartMoving", 5f);
        }
        if (other.CompareTag("Key"))
        {
            Debug.Log("AI picked up the key");
            hasKey = true;
            key.SetActive(false);
            GetComponent<AIMovement>().Invoke("StartMoving", 5f);
        }
    }
}
