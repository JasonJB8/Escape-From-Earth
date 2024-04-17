using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBehavior : MonoBehaviour
{
    public enum FSMStates
    {
        Idle, Patrol, Chase
    }

    public FSMStates currentState;
    public float enemySpeed = 5;
    GameObject[] wanderPoints;
    Vector3 nextDestination;
    Animator animator;
    int currentDestinationIndex = 0;
    float elapsedTime = 0;
    float rabbitIdleTime = 2f;
    void Start()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("RabbitWanderPoint");
        animator = GetComponent<Animator>();
        
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {

        switch(currentState)
        {
            case FSMStates.Idle:
                UpdateIdleState();
                break;
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
        }
    }

    void Initialize()
    {
        currentState = FSMStates.Idle;
        FindNextPoint();
    }

    void UpdateIdleState()
    {
        elapsedTime += Time.deltaTime;
        animator.SetInteger("animState", 0);
        if(elapsedTime >= rabbitIdleTime) {
            elapsedTime = 0;
            currentState = FSMStates.Patrol;
        }
    }

    void UpdatePatrolState()
    {
        animator.SetInteger("animState", 1);

        if(Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
            currentState = FSMStates.Idle;
        }


        FaceTarget(nextDestination);
        transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;
        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

}
