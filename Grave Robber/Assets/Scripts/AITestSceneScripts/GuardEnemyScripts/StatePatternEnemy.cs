using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatePatternEnemy : MonoBehaviour
{    
    public float searchTurnSpeed;   //Nopeus mill� Enemy K��ntyy alert tilassa
    public float searchingDuration; 
    public float sightRange;
    public Transform[] waypoints;
    public Transform eye;   //Pallosilm�, t�st� l�htee n�k�s�de
    public Transform enemy; // MUUTOS
    public MeshRenderer Indicator;  //Laatikko Enemyn p��ll�, Muuttaa v�ri� tilan mukaan, Debuggitarkoitus.
    public GameObject picture;    

    [HideInInspector] public Transform chaseTarget; //Kun Enemy jahtaa, t�m� on kohde. Yleens� Player.
    [HideInInspector] public IEnemyState currentState;  //T�h�n tallennetaan voimassaoleva tila
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public ChaseState chaseState;

    [HideInInspector] public AttackState attackState; //ATTACK muutokset
    // MUUTETTU
    public Vector3 lastKnownPlayerPosition;

    // MUUTETTU
    [HideInInspector] public TrackingState trackingState;

    [HideInInspector] public NavMeshAgent navMeshAgent; //NavMeshAgent komponentti

    private void Awake()
    {
        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        chaseState = new ChaseState(this);
        attackState = new AttackState(this);
        // MUUTETTU
        trackingState = new TrackingState(this);

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = patrolState;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();        
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            
            Debug.Log("T�rm�ys pelaajaan");
            enemy.GetComponent<CapsuleCollider>().enabled = false;
            GameObject.Find("EnemySound").GetComponents<AudioSource>()[0].Play();
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            gameObject.GetComponent<NavMeshAgent>().speed = 0;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<NavMeshAgent>().SetDestination(Vector3.zero);
            NewColor();
            StartCoroutine("Wait", 5f);
        }        
    }

    public void NewColor()
    {
        Debug.Log("uusi v�ri");
        picture.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    IEnumerator Wait(float waitTime)
    {
        //Debug.Log("Testi1");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Loading Stage");
        SceneManager.LoadScene(1);
        //SceneManager.LoadScene(0); 
    }
}
