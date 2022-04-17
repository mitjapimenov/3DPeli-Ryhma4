using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FollowPlayer : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;

    public GameObject picture;

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
            NewColor();
            StartCoroutine("Wait", 5f);
        }        
    }

    public void NewColor()
    {
        Debug.Log("uusi väri");
        picture.GetComponent<Image>().color = new Color32 (255, 255, 255, 255);
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
