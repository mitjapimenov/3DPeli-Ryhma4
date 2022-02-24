using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public GameObject player;
    private Transform playerPos;
    private Vector3 currentPos;
    public float distance;
    public float speedEnemy;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerPos.position) < distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos.position, speedEnemy * Time.deltaTime);
        }
        //else
        //{
        //    if(Vector3.Distance(transform.position, currentPos) <= 0)
        //    {

        //    }
        //    else
        //    {
        //        transform.position = Vector3.MoveTowards(transform.position, currentPos, speedEnemy * Time.deltaTime);
        //    }
        //}
    }
}
