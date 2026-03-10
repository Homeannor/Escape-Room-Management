using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class Opening_Interaction : MonoBehaviour
{
    private int reactionTime;
    private int randomNumber; //used for successRate
    public int successRate; //percentage for success (80 means 80% chance of opening)
    public GameObject openPrefab;
    public GameObject closedPrefab;
    public bool needsKey;

    private void OnTriggerEnter(Collider other)
    {
        reactionTime = Random.Range(1, 10); //waits for a random amount of time
        randomNumber = Random.Range(1, 100);
        
        if (other.CompareTag("Customer"))
        {
            //Debug.Log("Interacting with" + gameObject.name);
            other.GetComponent<AIMovement>().StopMoving();
            if (needsKey == true) //if items needs key to be opened
            {
                if (other.GetComponent<AIMovement>().hasKey == true)
                {
                    if (randomNumber < successRate)
                    {
                        //successfully opens with key and is happy
                        StartCoroutine(ChangePrefab());
                        other.GetComponent<Customer>().isHappy = true;
                        other.GetComponent<AIMovement>().hasKey = false;                    
                    }
                    else
                    {
                        //fails to open with key and is confused
                        other.GetComponent<Customer>().isConfused = true;
                        other.GetComponent<Customer>().confusedCounter++;
                    }
                }
                else
                {
                    //look for key
                    other.GetComponent<Customer>().hasIdea = true;
                }
            }
            else if (randomNumber < successRate) //if key is not needed just checks if the customer successfully interacts with object
            {
                //successfully opens and is happy
                StartCoroutine(ChangePrefab());
                other.GetComponent<Customer>().isHappy = true;
            }
            else
            {
                //fails to open and is confused
                other.GetComponent<Customer>().isConfused = true;
                other.GetComponent<Customer>().confusedCounter++;
            }

            other.GetComponent<AIMovement>().Invoke("StartMoving", reactionTime);
        }
    }

    private IEnumerator ChangePrefab()
    {
        yield return new WaitForSeconds(2);
        closedPrefab.SetActive(false);
        openPrefab.SetActive(true);
    }

}
