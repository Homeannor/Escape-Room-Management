using UnityEngine;

public class Key_Interaction : MonoBehaviour
{
    private int reactionTime;
    private void OnTriggerEnter(Collider other)
    {
        reactionTime = Random.Range(1, 10); //waits for a random amount of time
        if (other.CompareTag("Customer"))
        {
            if (other.GetComponent<AIMovement>().hasKey == true)
            {
                return;
            }
            else
            {
                //Debug.Log("Interacting with" + gameObject.name);
                other.GetComponent<AIMovement>().StopMoving();
                other.GetComponent<AIMovement>().hasKey = true;
                other.GetComponent<Customer>().hasIdea = true; //customer has the idea to use the key after picking it up
                gameObject.SetActive(false);
                other.GetComponent<AIMovement>().Invoke("StartMoving", reactionTime);
            }
        }
    }
}
