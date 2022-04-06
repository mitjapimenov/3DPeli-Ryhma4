using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HoldToPickup : MonoBehaviour
{
    public GameObject enemy; // Muutettu
    public GameObject enemySpawn;//Muutettu
    public GameObject enemySpawn2;//Muutettu

    [SerializeField]
    public Camera camera1;
    [SerializeField]
    private LayerMask layermask;
    [SerializeField]
    private float pickupTime = 2f;
    [SerializeField]
    private RectTransform pickupImageRoot;
    [SerializeField]
    private Image pickupProgressImage;
    [SerializeField]
    private TextMeshProUGUI itemNameText;

    private Item itemBeingPickedUp;
    private float currentPickUpTimerElapsed;


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
            GameObject projectile = Instantiate(enemy, enemySpawn.transform.position, Quaternion.identity); //Muutettu
            Destroy(itemBeingPickedUp.gameObject);
            itemBeingPickedUp = null;
        }
        else if (itemBeingPickedUp.name == "kultaharkot2") //Make These if statement what you wanna do for item
        {
            GameObject projectile = Instantiate(enemy, enemySpawn2.transform.position, Quaternion.identity); //Muutettu
            Destroy(itemBeingPickedUp.gameObject);
            itemBeingPickedUp = null;
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
                itemNameText.text = "Ker‰‰ " + itemBeingPickedUp.name;
            }
        }
        else
        {
            itemBeingPickedUp = null;
        }
    }
}
