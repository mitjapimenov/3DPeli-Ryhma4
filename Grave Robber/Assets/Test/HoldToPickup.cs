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
    public Animator anim5;
    public Animator anim6;
    public Animator anim7;

    public GameObject picture;

    public GameObject myEnemy;
    public bool activateEnemy1;

    public GameObject myEnemy2;
    public bool activateEnemy2;    

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
    private RectTransform openupImageRoot;
    [SerializeField]
    private Image pickupProgressImage;
    [SerializeField]
    private Image pickupProgressImage2;
    [SerializeField]    
    private TextMeshProUGUI itemNameText;
    [SerializeField]
    private TextMeshProUGUI itemNameText2;

    private Item itemBeingPickedUp;
    private float currentPickUpTimerElapsed;

    [SerializeField]
    private TextMeshProUGUI collectableText;
    [SerializeField]
    private TextMeshProUGUI nothingText;

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
            //Debug.Log("Testi");
            //pickupImageRoot.gameObject.SetActive(true);

            if (Input.GetButton("Fire1"))
            {
                if (pickupImageRoot.gameObject.activeSelf)
                {
                    GetComponent<PlayerController>().enabled = false;
                    GetComponent<AudioSource>().volume = 0f; // Footstep volume 0f when moving and try to dig same time
                    transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                    IncrementPickupProgressAndTryComplete();
                }
                else
                {
                    GetComponent<PlayerController>().enabled = false;
                    GetComponent<AudioSource>().volume = 0f; // Footstep volume 0f when moving and try to open same time
                    IncrementPickupProgressAndTryComplete();
                }
                
            }
            else
            {
                GetComponent<PlayerController>().enabled = true;
                transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                currentPickUpTimerElapsed = 0f;
            }

            UpdatePickupProgressImage();
        }
        //if (HasItemTargetted())
        //{
        //    Debug.Log("Testi2");
        //    openupImageRoot.gameObject.SetActive(true);

        //    if (Input.GetButton("Fire1"))
        //    {
        //        IncrementPickupProgressAndTryComplete();
        //    }
        //    else
        //    {
        //        currentPickUpTimerElapsed = 0f;
        //    }

        //    UpdatePickupProgressImage();
        //}
        else
        {
            GetComponent<PlayerController>().enabled = true; // MUUTETTU
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            pickupImageRoot.gameObject.SetActive(false);
            openupImageRoot.gameObject.SetActive(false);
            currentPickUpTimerElapsed = 0f;
        }
    }    

    private void UpdatePickupProgressImage()
    {
        float pct = currentPickUpTimerElapsed / pickupTime;
        pickupProgressImage.fillAmount = pct;

        float pct2 = currentPickUpTimerElapsed / pickupTime;
        pickupProgressImage2.fillAmount = pct2;
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
            //GameObject projectile = Instantiate(pelastys, pelastysSpawn.transform.position, Quaternion.identity);
            StartCoroutine("Wait2", 2f);
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
            //GameObject projectile = Instantiate(enemy, enemySpawn.transform.position, Quaternion.identity);
            if(activateEnemy1 == true)
            {
                myEnemy.SetActive(true);
            }
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
            Debug.Log("Tyhj‰ Aarre");
            StartCoroutine("Wait6", 2f);
            Destroy(itemBeingPickedUp.gameObject);
            itemBeingPickedUp = null;
        }
        else if (itemBeingPickedUp.name == "Cube2") //Make These if statement what you wanna do for item
        {
            //GameObject projectile = Instantiate(pelastys, pelastysSpawn2.transform.position, Quaternion.identity);
            Destroy(itemBeingPickedUp.gameObject);
            StartCoroutine("Wait3", 2f);
            itemBeingPickedUp = null;
        }
        else if (itemBeingPickedUp.name == "Cube3") //Make These if statement what you wanna do for item
        {
            //GameObject projectile = Instantiate(pelastys, pelastysSpawn3.transform.position, Quaternion.identity);
            Destroy(itemBeingPickedUp.gameObject);
            StartCoroutine("Wait4", 2f);
            itemBeingPickedUp = null;
        }
        else if (itemBeingPickedUp.name == "Enemy2") //Make These if statement what you wanna do for item
        {
            //GameObject projectile = Instantiate(enemy, enemySpawn2.transform.position, Quaternion.identity);
            if (activateEnemy2 == true)
            {
                myEnemy2.SetActive(true);
            }
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
            Destroy(itemBeingPickedUp.gameObject);
            itemBeingPickedUp = null;
            StartCoroutine("Wait", 5f);        // You can change the float to make coroutine faster or slower    
            GameObject.Find("OpenGate").GetComponents<AudioSource>()[0].Play();
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
            else if (hititem != null && hititem != itemBeingPickedUp && hititem.CompareTag("Item"))
            {
                pickupImageRoot.gameObject.SetActive(true);
                itemBeingPickedUp = hititem;
                itemNameText.text = "Search " /*+ itemBeingPickedUp.name*/;
            }
            else if (hititem != null && hititem != itemBeingPickedUp && hititem.CompareTag("exit"))
            {
                openupImageRoot.gameObject.SetActive(true);
                itemBeingPickedUp = hititem;
                itemNameText2.text = " Open " /*+ itemBeingPickedUp.name*/;
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
            collectableText.text = collectable + " / 3";
            StartCoroutine("Wait5", 3f);

            if (collectable == 3)
            {
                Debug.Log("you hear something far away...");
                GameObject exit1 = Instantiate(exit, exitSpawn.transform.position, Quaternion.identity); //Muutettu
                GameObject exit2 = Instantiate(exit, exitSpawn2.transform.position, Quaternion.identity); //Muutettu
                GameObject exit3 = Instantiate(exit, exitSpawn3.transform.position, Quaternion.identity); //Muutettu
                GameObject exit4 = Instantiate(exit, exitSpawn4.transform.position, Quaternion.identity); //Muutettu                
            }

        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("CollisionHoldTopickup");            
            GetComponent<AudioSource>().enabled = false;
        }
    }

    //public void NewColor()
    //{
    //    Debug.Log("Pel‰stys p‰‰lle");
    //    picture.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    //}

    //public void NewColor2()
    //{
    //    Debug.Log("Pel‰stys pois");
    //    picture.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    //}

    public void NewColor3()
    {
        Debug.Log("CollectableUIP‰‰lle");
        collectableText.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
    }

    public void NewColor4()
    {
        Debug.Log("CollectableUIpoisp‰‰lt‰");
        collectableText.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 0);
    }

    public void NewColor5()
    {
        Debug.Log("NothingTextUIP‰‰lle");
        nothingText.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
    }

    public void NewColor6()
    {
        Debug.Log("NothingTextUIpoisp‰‰lt‰");
        nothingText.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 0);
    }



    IEnumerator Wait(float waitTime)
    {
        //Debug.Log("Testi1");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("ExitStage");
        SceneManager.LoadScene(0);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    IEnumerator Wait2(float wait2Time)
    {
        //NewColor();
        //Debug.Log("Pel‰stytt‰j‰ picture");
        //Debug.Log("Testi1");
        anim5.SetBool("open", true);       
        yield return new WaitForSeconds(wait2Time);
        anim5.SetBool("open", false);
        //NewColor2();        
        //SceneManager.LoadScene(0); 
    }

    IEnumerator Wait3(float wait3Time)
    {
        //NewColor();
        //Debug.Log("Pel‰stytt‰j‰ picture");
        //Debug.Log("Testi1");
        anim6.SetBool("open", true);
        yield return new WaitForSeconds(wait3Time);
        anim6.SetBool("open", false);
        //NewColor2();        
        //SceneManager.LoadScene(0); 
    }

    IEnumerator Wait4(float wait4Time)
    {
        //NewColor();
        //Debug.Log("Pel‰stytt‰j‰ picture");
        //Debug.Log("Testi1");
        anim7.SetBool("open", true);
        yield return new WaitForSeconds(wait4Time);
        anim7.SetBool("open", false);
        //NewColor2();        
        //SceneManager.LoadScene(0); 
    }

    IEnumerator Wait5(float wait5Time)
    {
        NewColor3();
        yield return new WaitForSeconds(wait5Time);
        NewColor4();
    }

    IEnumerator Wait6(float wait6Time)
    {
        NewColor5();
        yield return new WaitForSeconds(wait6Time);
        NewColor6();
    }
}
