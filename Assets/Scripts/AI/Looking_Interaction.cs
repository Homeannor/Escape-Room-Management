using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Looking_Interaction : MonoBehaviour
{
    public bool alreadyLooked = false;
    private int reactionTime;
    private int randomNumber2;
    private int randomNumber;
    public int successRate; //used to make some props less obvious clues (lower number means the customer is less likely to stop and look at it)

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Interacting with" + gameObject.name);
        reactionTime = Random.Range(1, 10);
        randomNumber = Random.Range(1, 100);
        randomNumber2 = Random.Range(1, 100);
        if (other.CompareTag("Customer"))
        {
            if (randomNumber < successRate)
            {
                if (alreadyLooked == false)
                {
                    other.GetComponent<AIMovement>().StopMoving();
                    other.GetComponent<AIMovement>().Invoke("StartMoving", reactionTime);
                    alreadyLooked = true;
                    if (randomNumber2 < successRate)
                    {
                        other.GetComponent<Customer>().hasIdea = true;
                    }
                    else
                    {
                        other.GetComponent<Customer>().isConfused = true;
                        other.GetComponent<Customer>().confusedCounter++;
                    }
                }
                else
                {
                    StartCoroutine(ChangeBoolean());
                }
            }
        }
    }

    private IEnumerator ChangeBoolean()
    {
        yield return new WaitForSeconds(30);
        alreadyLooked = false;
    }
}
