using UnityEngine;

public class AIInteraction : MonoBehaviour
{
    public GameObject closedChest;
    public GameObject openChest;
    public GameObject closedTomb;
    public GameObject openTomb;
    public GameObject closedDoor;
    public GameObject openDoor;
    private void OnTriggerEnter(Collider other)
    {
        AIMovement.instance.StopMoving();
        if (other.CompareTag("Chest"))
        {
            //open chest
            closedChest.SetActive(false);
            openChest.SetActive(true);
            Invoke("AIMovement.instance.RandomMovement", 5f);
        }
        if (other.CompareTag("Tomb"))
        {
            //open tomb
            closedTomb.SetActive(false);
            openTomb.SetActive(true);
            Invoke("AIMovement.instance.RandomMovement", 5f);
        }
        if (other.CompareTag("Door"))
        {
            //open door
            closedDoor.SetActive(false);
            openDoor.SetActive(true);
            Invoke("AIMovement.instance.RandomMovement", 5f);
        }
    }
}
