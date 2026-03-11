using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Customer : MonoBehaviour
{
    public GameObject happyIcon;
    public GameObject angryIcon;
    public GameObject confusedIcon;
    public GameObject ideaIcon;
    public GameObject needsHintIcon;

    public bool isHappy;
    public bool isAngry;
    public bool isConfused;
    public bool hasIdea;
    public bool needsHint;

    public int confusedCounter;
    public int angryCounter;

    public void Start()
    {
        confusedCounter = 0;
        angryCounter = 0;
        GetComponent<NavMeshAgent>().speed = 10;
        happyIcon.SetActive(false);
        angryIcon.SetActive(false);
        confusedIcon.SetActive(false);
        ideaIcon.SetActive(false);
        needsHintIcon.SetActive(false);
    }

    private void Update()
    {
        //makes icons look at which camera is active
        if(SwitchCamera.instance.topDownCameraBool == true)
        {
            happyIcon.transform.LookAt(SwitchCamera.instance.topDcamera.transform);
            angryIcon.transform.LookAt(SwitchCamera.instance.topDcamera.transform);
            confusedIcon.transform.LookAt(SwitchCamera.instance.topDcamera.transform);
            ideaIcon.transform.LookAt(SwitchCamera.instance.topDcamera.transform);
            needsHintIcon.transform.LookAt(SwitchCamera.instance.topDcamera.transform);
        }
        else
        {
            happyIcon.transform.LookAt(SwitchCamera.instance.isocamera.transform);
            angryIcon.transform.LookAt(SwitchCamera.instance.isocamera.transform);
            confusedIcon.transform.LookAt(SwitchCamera.instance.isocamera.transform);
            ideaIcon.transform.LookAt(SwitchCamera.instance.isocamera.transform);
            needsHintIcon.transform.LookAt(SwitchCamera.instance.isocamera.transform);
        }
        if (confusedCounter >= 3) //if the customer gets confused too many times they will get angry
        {
            confusedCounter = 0;
            isConfused = false;
            isAngry = true;
            angryCounter++;
        }
        if (angryCounter >= 2) //if the customer gets angry too many times they will need a hint
        {
            angryCounter = 0;
            isAngry = false;
            needsHint = true;
        }
        if (isHappy == true)
        {
            happyIcon.SetActive(true);
            angryIcon.SetActive(false);
            confusedIcon.SetActive(false);
            ideaIcon.SetActive(false);
            needsHintIcon.SetActive(false);
            StartCoroutine(ChangeHappy());
        }
        else if (isConfused == true)
        {
            confusedIcon.SetActive(true);
            happyIcon.SetActive(false);
            angryIcon.SetActive(false);
            ideaIcon.SetActive(false);
            needsHintIcon.SetActive(false);
            StartCoroutine(ChangeConfused());
        }
        else if (isAngry == true)
        {
            angryIcon.SetActive(true);
            happyIcon.SetActive(false);
            confusedIcon.SetActive(false);
            ideaIcon.SetActive(false);
            needsHintIcon.SetActive(false);
            StartCoroutine(ChangeAngry());
        }
        else if (hasIdea == true)
        {
            ideaIcon.SetActive(true);
            happyIcon.SetActive(false);
            angryIcon.SetActive(false);
            confusedIcon.SetActive(false);
            needsHintIcon.SetActive(false);
            StartCoroutine(ChangeIdea());
        }
        else if (needsHint == true)
        {
            needsHintIcon.SetActive(true);
            happyIcon.SetActive(false);
            angryIcon.SetActive(false);
            confusedIcon.SetActive(false);
            ideaIcon.SetActive(false);
            GetComponent<NavMeshAgent>().speed = 0;   
            //Debug.Log("A customer needs a hint");
        }
        else
        {
            happyIcon.SetActive(false);
            angryIcon.SetActive(false);
            confusedIcon.SetActive(false);
            ideaIcon.SetActive(false);
            needsHintIcon.SetActive(false);
        }
    }

    private IEnumerator ChangeHappy()
    {
        yield return new WaitForSeconds(15);
        isHappy = false;
    }
    private IEnumerator ChangeAngry()
    {
        yield return new WaitForSeconds(15);
        isAngry = false;
    }
    private IEnumerator ChangeConfused()
    {
        yield return new WaitForSeconds(15);
        isConfused = false;
    }
    private IEnumerator ChangeIdea()
    {
        yield return new WaitForSeconds(15);
        hasIdea = false;
    }
   public void GiveHint()
    {
        Debug.Log("Gave Hint");
        needsHintIcon.SetActive(false);
        needsHint = false;
        GetComponent<NavMeshAgent>().speed = 10;
    }

}
