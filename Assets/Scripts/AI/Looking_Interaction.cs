using UnityEngine;
using UnityEngine.AI;

public class Looking_Interaction : MonoBehaviour
{
    private int reactionTime;

    private void OnTriggerEnter(Collider other)
    {
        reactionTime = Random.Range(1, 10); //waits for a random amount of time
        if (other.CompareTag("Customer"))
        {
            Debug.Log("Interacting with" + gameObject.name);
            other.GetComponent<AIMovement>().StopMoving();
            other.GetComponent<AIMovement>().Invoke("StartMoving", reactionTime);
        }
    }
}
