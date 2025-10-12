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
    private bool isChasing = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
            agent.speed = moveSpeed;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange)
        {
            isChasing = true;
            agent.SetDestination(player.position);
            animator.SetBool("isChasing", true);


            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            isChasing = false;
            animator.SetBool("isChasing", false);
            agent.ResetPath();
        }


        if (distance <= attackRange)
        {
        }
    }
}
