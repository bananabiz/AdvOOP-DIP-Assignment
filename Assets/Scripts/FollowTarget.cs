using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    public int dis = 50;
    public float radius = 8f;
    public float wanderSpeedRate = 0.3f;
    public GameObject target;

    private NavMeshAgent agent;
    private bool isWander = true;
    private Vector3 randomDir;
    private float distance;
    private float agentSpeed;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        // update player's position
        Vector3 targetPosition = target.transform.position;
        distance = Vector3.Distance(transform.position, targetPosition);

        // move enemy towards player if player close enough
        if (distance < dis)
        {
            // rotate enemy towards player
            transform.LookAt(target.transform);
            agent.speed = agentSpeed;  //reset speed to full speed
            agent.SetDestination(targetPosition);
        }

        if (distance > dis)
        {
            if (isWander)
            {
                GetRandomDirection();
                Debug.Log(randomDir);
                isWander = false;
            }

            if (isWander == false)
            {
                agent.speed = agentSpeed * wanderSpeedRate;  //set speed to wander speed
                agent.SetDestination(randomDir);
            }
            
            if (Vector3.Distance(transform.position, randomDir) < 0.5f)
            {
                isWander = true;
            }
        }
    }

    void GetRandomDirection()
    {
        float randX = Random.Range(-radius, radius);
        float randZ = Random.Range(-radius, radius);
        // Create a random direction vector
        randomDir = new Vector3(randX, 0, randZ) + transform.position;  
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Environment" && distance > dis)
        {
            GetRandomDirection();  //generate a new wander direction once hit the wall
        }
    } 
}
