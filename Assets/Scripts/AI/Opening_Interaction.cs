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
            if (needsKey == true)
            {
                if (other.GetComponent<AIMovement>().hasKey == true & randomNumber < successRate)
                {
                    StartCoroutine(ChangePrefab());
                    other.GetComponent<AIMovement>().hasKey = false;                    
                }
                else
                {
                    //emoji
                }
            }
            else
            {
                StartCoroutine(ChangePrefab());
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
