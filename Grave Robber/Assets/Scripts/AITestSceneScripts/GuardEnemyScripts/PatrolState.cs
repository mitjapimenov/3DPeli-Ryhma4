using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private StatePatternEnemy enemy;
    int nextWaypoint;

    public PatrolState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Patrol();
        Look();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ToAlertState();
        }
    }

    public void OnTriggerState()
    {
        
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

    public void ToPatrolState()
    {
       
    }

    void Look()
    {
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.green);

        RaycastHit hit;
        if(Physics.Raycast(enemy.eye.position, enemy.eye.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;

            ToChaseState();
        }
    }

    void Patrol()
    {
        enemy.Indicator.material.color = Color.green;
        enemy.navMeshAgent.destination = enemy.waypoints[nextWaypoint].position;
        enemy.navMeshAgent.isStopped = false;        

        if(enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            // kasvattaa nextWaypointtia, mutta kun nextWaypoint +1 = 4, on se sama arvo kuin enemy.waypoinst.Lenght
            // jolloin % eli jakojäännös on 0, eli enemy lähtee looppaamaan kohti ensimmäistä waypointtia. Vältetään if/else himmeli.
            nextWaypoint = (nextWaypoint + 1) % enemy.waypoints.Length;
        }
    }

    //MUUTETTU
    public void ToTrackingState()
    {

    }

    public void ToAttackState()
    {
        //Attack muutokset
    }
}
