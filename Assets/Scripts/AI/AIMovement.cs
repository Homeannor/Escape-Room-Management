using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public static AIMovement instance;
    private NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer;
    private Vector3 targetLocation;
    private bool hasTargetLocation;
    [SerializeField] float range;

    private void Start()
    {
        instance = this;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        RandomMovement();
    }
    void RandomMovement()
    {
        gameObject.GetComponent<NavMeshAgent>().speed = 10;
        if (!hasTargetLocation)
        {
            SearchForTarget();
        }
        if (hasTargetLocation)
        {
            agent.SetDestination(targetLocation);
            float distance = Vector3.Distance(transform.position, targetLocation);
            if (distance < 5f)
            {
                hasTargetLocation = false;
            }
        }
    }

    void SearchForTarget()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);
        targetLocation = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        if (Physics.Raycast(targetLocation, Vector3.down, groundLayer))
        {
            hasTargetLocation = true;
        }
    }

    public void StopMoving()
    {
        gameObject.GetComponent<NavMeshAgent>().speed = 0;
    }
}
