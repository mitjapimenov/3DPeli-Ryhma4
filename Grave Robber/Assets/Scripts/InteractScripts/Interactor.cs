using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{

    public LayerMask interactableLayermask = 6;
    Interactable interactable;
    public Image interactImage;
    public Sprite defaultIcon;
    public Vector2 defaultIconSize;
    public Sprite defaultInteractIcon;
    public Vector2 defaultInteractIconSize;

    public bool isOpen; // Muutettu
    public GameObject enemy; // Muutettu
    public GameObject enemySpawn;//Muutettu

    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2.3f , interactableLayermask))
                                                                                                //Interact pituus 
        {
           if (hit.collider.GetComponent<Interactable>() != false)
           {
               if(interactable == null || interactable.ID != hit.collider.GetComponent<Interactable>().ID)
               {
                    interactable = hit.collider.GetComponent<Interactable>();
               }

                if(interactable.interactIcon != null)
                {
                    interactImage.sprite = interactable.interactIcon;  
                    if(interactable.iconSize == Vector2.zero)
                    {
                        interactImage.rectTransform.sizeDelta = defaultInteractIconSize;
                    }
                    else 
                    {
                        interactImage.rectTransform.sizeDelta = interactable.iconSize;
                    }
                }

                else
                {
                    interactImage.sprite = defaultInteractIcon;
                    interactImage.rectTransform.sizeDelta = defaultInteractIconSize;
                }

               if (!isOpen && Input.GetKeyDown(KeyCode.E)) // !isOpen muutos
               {
                    Debug.Log("Aarre avattu"); // Muutettu
                    isOpen = true; // Muutettu
                    interactable.onInteract.Invoke();
                    GameObject projectile = Instantiate(enemy, enemySpawn.transform.position, Quaternion.identity); //Muutettu
                }
           }
        }
        else 
        {
            if (interactImage.sprite != defaultIcon)
            {
                interactImage.sprite = defaultIcon;
                interactImage.rectTransform.sizeDelta = defaultIconSize;
            }
        }
    }




}
