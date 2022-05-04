using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{    
    private StatePatternEnemy enemy;

    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Chase();
    }

    public void OnTriggerEnter(Collider other)
    {
        
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

    }

    public void ToPatrolState()
    {
        
    }

    void Look()
    {
        Vector3 enemyToTarget = enemy.chaseTarget.position - enemy.eye.position;

        Debug.DrawRay(enemy.eye.position, enemyToTarget, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(enemy.eye.position, enemyToTarget, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            enemy.navMeshAgent.speed = 7f;
            GameObject.Find("Vartija_animaatio").GetComponent<Animator>().Play("Armature|Run");
            //enemy.animator.SetBool("run", false);
        }
        else
        {
            enemy.lastKnownPlayerPosition = enemy.chaseTarget.position;
            enemy.navMeshAgent.speed = 3f;
            ToTrackingState();
        }
    }

    /*
        Kotil‰ksy:
        Tee nelj‰s tila nimelt‰ TrackingState. Kun Player h‰vi‰‰ Chase tilassa n‰kyvist‰. Enemy tallentaa pelaaja
        viimeisimm‰n tiedossa olevan sijainnin muistiin.
        Enemy siirtyy Tracking tilaan ja hakeutuu NavMeshin Pelaajan viimeisimp‰‰n sijaintiin. Kun Enemy p‰‰see perille, siirtyy se Alert tilaan.
        Teht‰v‰n suoritus edellytt‰‰ hieman kaikkien scriptien p‰ivityst‰. TrackingState tiedosto pit‰‰ luoda.
     */

    void Chase()
    {
        enemy.Indicator.material.color = Color.red;
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.isStopped = false;
        //enemy.navMeshAgent.speed = 4f;
    }

    //MUUTETTU
    public void ToTrackingState()
    {
        enemy.currentState = enemy.trackingState;
    }

    public void ToAttackState()
    {
        //Attack muutokset
    }


}
