using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class HoldToPickup : MonoBehaviour
{
    public GameObject enemy; // Muutettu
    public GameObject enemySpawn;//Muutettu
    public GameObject enemySpawn2;//Muutettu    

    public GameObject exit;
    public GameObject exitSpawn;
    public GameObject exitSpawn2;
    public GameObject exitSpawn3;
    public GameObject exitSpawn4;

    public int collectable;
    public GameObject collectableItem;
    public GameObject collectableSpawn;
    public GameObject collectableSpawn2;
    public GameObject collectableSpawn3;

    public GameObject pelastys;
    public GameObject pelastysSpawn;
    public GameObject pelastysSpawn2;
    public GameObject pelastysSpawn3;

    public Animator anim1;
    public Animator anim2;
    public Animator anim3;
    public Animator anim4;

    //public AudioClip collectableSound;

    [SerializeField]
    public Camera camera1;
    [SerializeField]
    private LayerMask layermask;
    [SerializeField]
    private float pickupTime = 10f; //Muutettu nyt 10f ja sama Player Hierarchy kohdassa.
    [SerializeField]
    private RectTransform pickupImageRoot;
    [SerializeField]
    private Image pickupProgressImage;
    [SerializeField]
    private TextMeshProUGUI itemNameText;

    private Item itemBeingPickedUp;
    private float currentPickUpTimerElapsed;

    private void Start()
    {
        //StartCoroutine("Wait", 5f);
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectItemBeingPickedupFromRay();

        if (HasItemTargetted())
        {
            pickupImageRoot.gameObject.SetActive(true);

            if (Input.GetButton("Fire1"))
            {
                IncrementPickupProgressAndTryComplete();                
            }
            else
            {
                currentPickUpTimerElapsed = 0f;
            }

            UpdatePickupProgressImage();
        }
        else
        {
            pickupImageRoot.gameObject.SetActive(false);
            currentPickUpTimerElapsed = 0f;
        }
    }

    private void UpdatePickupProgressImage()
    {
        float pct = currentPickUpTimerElapsed / pickupTime;
        pickupProgressImage.fillAmount = pct;
    }

    private void IncrementPickupProgressAndTryComplete()
    {
        currentPickUpTimerElapsed += Time.deltaTime;
        if (currentPickUpTimerElapsed >= pickupTime)
        {
            MoveItemToInventory();
        }
    }

    private void MoveItemToInventory()
    {
        if (itemBeingPickedUp.name == "kultaharkot1") //Make These if statement what you wanna do for item
        {            
            GameObject projectile = Instantiate(collectableItem, collectableSpawn.transform.position, Quaternion.identity); //Muutettu
            Destroy(itemBeingPickedUp.gameObject);
            itemBeingPickedUp = null;
        }
        else if (itemBeingPickedUp.name == "Cube1") //Make These if statement what you wanna do for item
        {
            GameObject projectile = Instantiate(pelastys, pelastysSpawn.transform.position, Quaternion.identity); //Muutettu
            Destroy(itemBeingPickedUp.gameObject);
            itemBeingPickedUp = null;
        }
        else if (itemBeingPickedUp.name == "kultaharkot2") //Make These if statement what you wanna do for item
        {
            GameObject projectile = Instantiate(collectableItem, collectableSpawn2.transform.position, Quaternion.identity); //Muutettu
            Destroy(itemBeingPickedUp.gameObject);
            itemBeingPickedUp = null;
        }
        else if (itemBeingPickedUp.name == "Enemy1") //Make These if statement what you wanna do for item
        {
            GameObject projectile = Instantiate(enemy, enemySpawn.transform.position, Quaternion.identity); //Muutettu
            Destroy(itemBeingPickedUp.gameObject);
            itemBeingPickedUp = null;
        }
        else if (itemBeingPickedUp.name == "kultaharkot3") //Make These if statement what you wanna do for item
        {
            GameObject projectile = Instantiate(collectableItem, collectableSpawn3.transform.position, Quaternion.identity); //Muutettu
            Destroy(itemBeingPickedUp.gameObject);
            itemBeingPickedUp = null;
        }
        else if (itemBeingPickedUp.name == "Empty") //Make These if statement what you wanna do for item
        {
            Debug.Log("Tyhjä Aarre");
            Destroy(itemBeingPickedUp.gameObject);
            itemBeingPickedUp = null;
        }
        else if (itemBeingPickedUp.name == "Cube2") //Make These if statement what you wanna do for item
        {
            GameObject projectile = Instantiate(pelastys, pelastysSpawn2.transform.position, Quaternion.identity); //Muutettu
            Destroy(itemBeingPickedUp.gameObject);
            itemBeingPickedUp = null;
        }
        else if (itemBeingPickedUp.name == "Cube3") //Make These if statement what you wanna do for item
        {
            GameObject projectile = Instantiate(pelastys, pelastysSpawn3.transform.position, Quaternion.identity); //Muutettu
            Destroy(itemBeingPickedUp.gameObject);
            itemBeingPickedUp = null;
        }
        else if (itemBeingPickedUp.name == "Enemy2") //Make These if statement what you wanna do for item
        {
            GameObject projectile = Instantiate(enemy, enemySpawn2.transform.position, Quaternion.identity); //Muutettu
            Destroy(itemBeingPickedUp.gameObject);
            itemBeingPickedUp = null;
        }
        else if (itemBeingPickedUp.name == "ExitTheStage(Clone)") //Make These if statement what you wanna do for item
        {
            anim1.SetBool("open", true);
            //anim1.Play("Gate_Open_anim");
            anim2.SetBool("open", true);
            //anim2.Play("Gate_Open_anim");
            anim3.SetBool("open", true);
            //anim3.Play("Gate_Open_anim");
            anim4.SetBool("open", true);
            //anim4.Play("Gate_Open_anim");
            StartCoroutine("Wait", 5f);        // You can change the float to make coroutine faster or slower    
        }
    }

    private bool HasItemTargetted()
    {
        return itemBeingPickedUp != null;
    }

    private void SelectItemBeingPickedupFromRay()
    {
        Ray ray = camera1.ViewportPointToRay(Vector3.one / 2f);

        RaycastHit hitinfo;
        if(Physics.Raycast(ray, out hitinfo, 2f, layermask)) //2f you can change how far the ray hits
        {
            var hititem = hitinfo.collider.GetComponent<Item>();

            if (hititem == null)
            {
                itemBeingPickedUp = null;
            }
            else if (hititem != null && hititem != itemBeingPickedUp)
            {
                itemBeingPickedUp = hititem;
                itemNameText.text = "Search " /*+ itemBeingPickedUp.name*/;
            }
        }
        else
        {
            itemBeingPickedUp = null;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            //AudioSource.PlayClipAtPoint(collectableSound, transform.position);
            GameObject.Find("CollectSound").GetComponents<AudioSource>()[0].Play();
            Destroy(other.gameObject);
            collectable += 1;

            if (collectable == 3)
            {
                Debug.Log("you here something far away...");
                GameObject exit1 = Instantiate(exit, exitSpawn.transform.position, Quaternion.identity); //Muutettu
                GameObject exit2 = Instantiate(exit, exitSpawn2.transform.position, Quaternion.identity); //Muutettu
                GameObject exit3 = Instantiate(exit, exitSpawn3.transform.position, Quaternion.identity); //Muutettu
                GameObject exit4 = Instantiate(exit, exitSpawn4.transform.position, Quaternion.identity); //Muutettu                
            }

        }
    }

    IEnumerator Wait(float waitTime)
    {
        //Debug.Log("Testi1");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("ExitStage");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Load new stage demos end screen
    }
}
