using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySleepingAI : MonoBehaviour
{
    public enum FSMStates
    {
        Idle, Patrol, Chase, Attack, Dead
    }

    public FSMStates currentState;
    public float enemySpeed = 5;
    public int damageAmount = 20;
    public float chaseDistance = 20;
    public float attackDistance = 2;
    public GameObject player;
    public float attackRate = 2;
    public GameObject deadVFX;
    public AudioClip deathSFX;

    GameObject[] wanderPoints;
    Vector3 nextDestination;
    Animator animator;
    float distanceToPlayer;
    int currentDestinationIndex = 0;
    EnemyHealth enemyHealth;
    int health;
    float elapsedTime;
    Transform deadTransform;
    bool isDead = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        animator = GetComponent<Animator>();
        

        enemyHealth = GetComponent<EnemyHealth>();
        health = enemyHealth.currentHealth;
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        health = enemyHealth.currentHealth;

        switch(currentState)
        {
            case FSMStates.Patrol:
                UpdateIdleState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;
        }

        elapsedTime += Time.deltaTime;

        if(health <= 0)
        {
            currentState = FSMStates.Dead;
        }
    }

    void Initialize()
    {
        currentState = FSMStates.Patrol;
        FindNextPoint();
    }

    void UpdateIdleState()
    {
        //print("zZzZzZzz");
        animator.SetInteger("animState", 0);

        if(distanceToPlayer <= chaseDistance && 
        Math.Abs(transform.position.y - player.transform.position.y)  < 3)
        {
            currentState = FSMStates.Chase;
        }

    }

    void UpdateChaseState()
    {
        //print("Chasing!");
        animator.SetInteger("animState", 1);
        nextDestination = player.transform.position;

        if(distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if(distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }


        FaceTarget(nextDestination);
        transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
    }

    void UpdateAttackState()
    {
        //print("Attack!");
        nextDestination = player.transform.position;

        if(distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if(distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if(distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);
        animator.SetInteger("animState", 2);
        EnemyAttack();
    }

    void EnemyAttack()
    {
        if(!isDead)
        {
            if(elapsedTime >= attackRate)
            {
                var animDuration = animator.GetCurrentAnimatorStateInfo(0).length;
                Invoke("Attacking", animDuration);
                elapsedTime = 0.0f;
            }
        }
    }

    void Attacking()
    {
        var playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(damageAmount);
    }

    void UpdateDeadState()
    {
        animator.SetInteger("animState", 3);
        isDead = true;
        deadTransform = gameObject.transform;
        //print("HISSSSsss...");

        Destroy(gameObject, 3);
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

    private void OnDestroy()
    {
        Instantiate(deadVFX, deadTransform.position, deadTransform.rotation);
    }
    
    private void OnDrawGizmos()
    {
        //attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        //chase
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
