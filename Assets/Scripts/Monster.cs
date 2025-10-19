using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player;             
    public float chaseRange = 10f;       
    public float attackRange = 1.5f;    
    public float moveSpeed = 3.5f;      

    private Animator animator;
    private NavMeshAgent agent;
    private bool isWalking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
            agent.speed = moveSpeed;
    }

    void Update()
    {
        if (player == null || agent == null || animator == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange)
        {
            agent.SetDestination(player.position);

            if (!isWalking)
            {
                animator.SetBool("isWalking", true);
                isWalking = true;
            }

            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {

            agent.ResetPath();

            if (isWalking)
            {
                animator.SetBool("isWalking", false);
                isWalking = false;
            }
        }


        if (distance <= attackRange)
        {

        }
        else
        {
            agent.isStopped = false;
        }
    }
}
