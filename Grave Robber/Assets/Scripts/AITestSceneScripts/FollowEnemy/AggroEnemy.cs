using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AggroEnemy : MonoBehaviour
{
    //public float patrolTime = 15; time in seconds to wait before seeking a new patrol destination
    public float aggroRange = 10; // distance in scene units below which the NPC will increase speed and seek the player
    //public Transform[] waypoints; collection of waypoints which define a patrol area

    int index; // the current waypoint index in the waypoints array
    float speed, agentSpeed; // current agent speed and NavMeshAgent component speed
    Transform player; // reference to the player object transform

    //Animator animator; reference to the animator component
    NavMeshAgent agent; // reference to the NavMeshAgent

    void Awake()
    {
        //animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null) { agentSpeed = agent.speed; }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //index = Random.Range(0, waypoints.Length);

        InvokeRepeating("Tick", 0, 0.5f);

        //if (waypoints.Length > 0)
        //{
        //    InvokeRepeating("Patrol", Random.Range(0, patrolTime), patrolTime);
        //}
    }

    void Update()
    {
        //animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    void Patrol()
    {
        //index = index == waypoints.Length - 1 ? 0 : index + 1;
    }

    void Tick()
    {
        //agent.destination = waypoints[index].position;
        //agent.speed = agentSpeed / 2;

        if (player != null && Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            agent.destination = player.position;
            agent.speed = agentSpeed;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("T�rm�ys pelaajaan");
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            gameObject.GetComponent<NavMeshAgent>().speed = 0;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<NavMeshAgent>().SetDestination(Vector3.zero);
        }
    }
}
