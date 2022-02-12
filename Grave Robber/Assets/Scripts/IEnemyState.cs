using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void UpdateState();

    void OnTriggerState();

    void OnTriggerEnter(Collider other);

    void ToPatrolState();

    void ToAlertState();

    void ToChaseState();

    //MUUTETTU
    void ToTrackingState();

    void ToAttackState();      //ATTACK muutokset

}
