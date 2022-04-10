using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.position);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Törmäys pelaajaan");
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            gameObject.GetComponent<NavMeshAgent>().speed = 0;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<NavMeshAgent>().SetDestination(Vector3.zero);
        }
    }
}
