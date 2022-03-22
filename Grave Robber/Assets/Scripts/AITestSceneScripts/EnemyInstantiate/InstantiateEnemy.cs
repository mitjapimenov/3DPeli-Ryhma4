using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEnemy : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemySpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(enemy, enemySpawn.transform.position, Quaternion.identity);
        }        
    }
}
